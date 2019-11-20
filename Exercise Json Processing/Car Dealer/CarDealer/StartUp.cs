using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new CarDealerContext())
            {
                //var inputJson = File.ReadAllText("./../../../Datasets/sales.json");

                var result = GetSalesWithAppliedDiscount(db);

                Console.WriteLine(result);
            }
        }

        //Problem 09
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliersToImport = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliersToImport);
            context.SaveChanges();

            return $"Successfully imported {suppliersToImport.Count}.";
        }

        //Problem 10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var partsToImport = JsonConvert.DeserializeObject<List<Part>>(inputJson).Where(p => p.SupplierId <= 31).ToList();

            context.Parts.AddRange(partsToImport);
            context.SaveChanges();

            return $"Successfully imported {partsToImport.Count}.";
        }

        //Problem 11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsToImport = JsonConvert.DeserializeObject<List<CarsImportDTO>>(inputJson);

            var cars = new List<Car>();
            var carParts = new List<PartCar>();

            foreach (var carToImport in carsToImport)
            {
                var car = new Car()
                {
                    Make = carToImport.Make,
                    Model = carToImport.Model,
                    TravelledDistance = carToImport.TravelledDistance
                };

                foreach (var part in carToImport.PartsId.Distinct())
                {
                    var carPart = new PartCar()
                    {
                        PartId = part,
                        Car = car
                    };

                    carParts.Add(carPart);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(carParts);
            context.SaveChanges();

            return $"Successfully imported {carsToImport.Count}.";
        }

        //Problem 12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customersToImport = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customersToImport);
            context.SaveChanges();

            return $"Successfully imported {customersToImport.Count}.";
        }

        //Problem 13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var salesToImport = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(salesToImport);
            context.SaveChanges();

            return $"Successfully imported {salesToImport.Count}.";
        }

        //Problem 14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context
                .Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver == true)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("d", new CultureInfo("fr-FR")),
                    c.IsYoungDriver
                })
                .ToList();

            var customersToExport = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return customersToExport;
        }

        //Problem 15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context
                .Cars
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            var carsToExport = JsonConvert.SerializeObject(toyotaCars, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            return carsToExport;
        }

        //Problem 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var dataToExport = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);

            return dataToExport;
        }

        //Problem 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsToExport = context
                .Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars.Select(pc => new
                    {
                        pc.Part.Name,
                        Price = pc.Part.Price.ToString("f2")
                    })
                    .ToList()
                })
                .ToList();

            var dataToExport = JsonConvert.SerializeObject(carsToExport, Formatting.Indented);

            return dataToExport;
        }

        //Problem 18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customerSales = context
                .Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(cs => cs.Part.Price))
                })
                .OrderBy(c => c.BoughtCars)
                .ThenBy(c => c.SpentMoney)
                .ToList();

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var jsonToExport = JsonConvert.SerializeObject(customerSales, new JsonSerializerSettings
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented
            });

            return jsonToExport;
        }

        //Problem 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("f2"),
                    price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("f2"),

                    priceWithDiscount = $@"{(s.Car.PartCars.Sum(p => p.Part.Price) -
                        s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100):F2}"
                })
                .Take(10)
                .ToList();

            //var resolver = new DefaultContractResolver
            //{
            //    NamingStrategy = new CamelCaseNamingStrategy()
            //};

            var exportData = JsonConvert.SerializeObject(sales, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            return exportData;
        }
    }
}