namespace ProductShop
{
    using System.Linq;

    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;

    using AutoMapper;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //Import
            this.CreateMap<ImportUserDTO, User>();
            this.CreateMap<ImportProductDTO, Product>();
            this.CreateMap<ImportCategoryDTO, Category>();
            this.CreateMap<ImportCategoryProductDTO, CategoryProduct>();

            //Export
            this.CreateMap<Product, ExportProductInRangeDTO>()
                .ForMember(x => x.Buyer, y => y.MapFrom(x => x.Buyer.FirstName + " " + x.Buyer.LastName));

            this.CreateMap<Product, ExportSoldProductDTO>();
            this.CreateMap<User, ExportUserSoldProductsDTO>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(x => x.ProductsSold));

            this.CreateMap<Category, ExportCategoriesByProductDTO>()
                .ForMember(x => x.Count, y => y.MapFrom(x => x.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(x => x.CategoryProducts.Average(ct => ct.Product.Price)))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(x => x.CategoryProducts.Sum(ct => ct.Product.Price)));
        }
    }
}
