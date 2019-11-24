namespace CarDealer
{
    using System.Linq;

    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;

    using AutoMapper;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDTO, Supplier>();

            this.CreateMap<ImportPartDTO, Part>();

            this.CreateMap<ImportCarDTO, Car>();

            this.CreateMap<ImportCustomerDTO, Customer>();

            this.CreateMap<ImportSaleDTO, Sale>();

            this.CreateMap<Car, ExportCarWithDistanceDTO>();

            this.CreateMap<Car, ExportCarMakeBMWDTO>();

            this.CreateMap<Supplier, ExportLocalSupplierDTO>()
                .ForMember(x => x.PartsCount, y => y.MapFrom(x => x.Parts.Count));

            this.CreateMap<Customer, ExportTotalSalesByCustomerDTO>()
                .ForMember(x => x.FullName, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.BoughtCars, y => y.MapFrom(x => x.Sales.Count))
                .ForMember(x => x.SpentMoney, y => y.MapFrom(x => x.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))));

            this.CreateMap<Car, ExportCarWithItsPartsDTO>()
                .ForMember(x => x.Parts, y => y.MapFrom(x => x.PartCars.Select(pc => pc.Part).OrderByDescending(p=>p.Price)));

            this.CreateMap<Part, ExportCarPartDTO>();
        }
    }
}
