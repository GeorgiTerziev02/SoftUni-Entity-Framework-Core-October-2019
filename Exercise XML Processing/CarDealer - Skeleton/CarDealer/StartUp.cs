namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            using (var db = new CarDealerContext())
            {
                //var inputXML = File.ReadAllText("./../../../Datasets/sales.xml");

                var result = GetCarsWithTheirListOfParts(db);

                Console.WriteLine(result);
            }
        }

        //Problem 09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ImportSupplierDTO>), new XmlRootAttribute("Suppliers"));

            List<ImportSupplierDTO> supplierDTOs;

            using (var reader = new StringReader(inputXml))
            {
                supplierDTOs = (List<ImportSupplierDTO>)xmlSerializer.Deserialize(reader);
            }

            var suppliers = Mapper.Map<List<Supplier>>(supplierDTOs);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        //Problem 10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportPartDTO>), new XmlRootAttribute("Parts"));

            List<ImportPartDTO> partsDTOs = (List<ImportPartDTO>)serializer.Deserialize(new StringReader(inputXml));

            var parts = Mapper.Map<List<Part>>(partsDTOs.Where(x => context.Suppliers.Any(s => s.Id == x.SupplierId)));

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        //Problem 11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportCarDTO>), new XmlRootAttribute("Cars"));

            List<ImportCarDTO> carDTOs;

            using (var reader = new StringReader(inputXml))
            {
                carDTOs = ((List<ImportCarDTO>)serializer.Deserialize(reader));
            }

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach (var carDTO in carDTOs)
            {
                var car = new Car()
                {
                    Make = carDTO.Make,
                    Model = carDTO.Model,
                    TravelledDistance = carDTO.TravelledDistance
                };

                var parts = carDTO
                    .Parts
                    .Select(p => p.Id)
                    .Where(p => context.Parts.Any(part => part.Id == p))
                    .Distinct();

                foreach (var partId in parts)
                {
                    var carPart = new PartCar()
                    {
                        PartId = partId,
                        Car = car
                    };

                    partCars.Add(carPart);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        //Problem 12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportCustomerDTO>), new XmlRootAttribute("Customers"));

            var customerDTOs = new List<ImportCustomerDTO>();

            using (var reader = new StringReader(inputXml))
            {
                customerDTOs = (List<ImportCustomerDTO>)serializer.Deserialize(reader);
            }

            var customers = Mapper.Map<List<Customer>>(customerDTOs);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //Problem 13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportSaleDTO>), new XmlRootAttribute("Sales"));

            var salesDTOs = new List<ImportSaleDTO>();

            using (var reader = new StringReader(inputXml))
            {
                salesDTOs = ((List<ImportSaleDTO>)serializer.Deserialize(reader));
            }

            var sales = Mapper.Map<List<Sale>>(salesDTOs.Where(s => context.Cars.Any(c => c.Id == s.CarId)));

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        //Problem 14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var carsToExport = context
                .Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarWithDistanceDTO>()
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarWithDistanceDTO[]),
                                                            new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writter = new StringWriter(sb))
            {
                serializer.Serialize(writter, carsToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var bmwCars = context
                .Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<ExportCarMakeBMWDTO>()
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarMakeBMWDTO[]), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, bmwCars, namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var supplierToExport = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .ProjectTo<ExportLocalSupplierDTO>()
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportLocalSupplierDTO[]), new XmlRootAttribute("suppliers"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var writer= new StringWriter(sb))
            {
                serializer.Serialize(writer,supplierToExport,namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var sb = new StringBuilder();



            return sb.ToString().TrimEnd();
        }

        //Problem 18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var customersToExport = context
                .Customers
                .Where(c => c.Sales.Any())
                .ProjectTo<ExportTotalSalesByCustomerDTO>()
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportTotalSalesByCustomerDTO[]), new XmlRootAttribute("customers"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, customersToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}