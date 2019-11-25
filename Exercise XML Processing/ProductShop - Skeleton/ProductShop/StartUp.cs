namespace ProductShop
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using ProductShop.Data;
    using ProductShop.Models;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            //var inputXml = File.ReadAllText("./../../../Datasets/categories-products.xml");

            using (var db = new ProductShopContext())
            {
                var result = GetUsersWithProducts(db);

                Console.WriteLine(result);
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportUserDTO[]), new XmlRootAttribute("Users"));

            ImportUserDTO[] userDtos;

            using (var reader = new StringReader(inputXml))
            {
                userDtos = (ImportUserDTO[])serializer.Deserialize(reader);
            }

            var users = Mapper.Map<User[]>(userDtos);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportProductDTO[]), new XmlRootAttribute("Products"));

            ImportProductDTO[] productDTOs;

            using (var reader = new StringReader(inputXml))
            {
                productDTOs = (ImportProductDTO[])serializer.Deserialize(reader);
            }

            var products = Mapper.Map<Product[]>(productDTOs);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCategoryDTO>), new XmlRootAttribute("Categories"));

            List<ImportCategoryDTO> categoryDTOs;

            using (var reader = new StringReader(inputXml))
            {
                categoryDTOs = (List<ImportCategoryDTO>)serializer.Deserialize(reader);
            }

            var categories = Mapper.Map<List<Category>>(categoryDTOs.Where(c => c.Name != null).ToList());

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCategoryProductDTO>), new XmlRootAttribute("CategoryProducts"));

            List<ImportCategoryProductDTO> categoryProductsDTOs;

            using (var reader = new StringReader(inputXml))
            {
                categoryProductsDTOs = (List<ImportCategoryProductDTO>)serializer.Deserialize(reader);
            }

            var categoryProducts = Mapper.Map<List<CategoryProduct>>(categoryProductsDTOs
                                                    .Where(ct => context.Categories.Any(c => c.Id == ct.CategoryId))
                                                    .Where(ct => context.Products.Any(p => p.Id == ct.ProductId)).ToList());

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ExportProductInRangeDTO>), new XmlRootAttribute("Products"));

            var productToExport = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .ProjectTo<ExportProductInRangeDTO>()
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, productToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var productToExport = context
                .Users
                .Where(u => u.ProductsSold.Any())
                .ProjectTo<ExportUserSoldProductsDTO>()
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ExportUserSoldProductsDTO>), new XmlRootAttribute("Users"));

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, productToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categoriesToExport = context
                .Categories
                .ProjectTo<ExportCategoriesByProductDTO>()
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ExportCategoriesByProductDTO>),
                                                            new XmlRootAttribute("Categories"));

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, categoriesToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u=>u.ProductsSold.Any())
                .OrderByDescending(u => u.ProductsSold.Count())
                .Select(u => new ExportUserAndHisProductsDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new ExportSoldProductsDTO
                                    {
                                        Count = u.ProductsSold.Count,
                                        SoldProducts = u.ProductsSold.Select(ps => new ExportSoldProductDTO
                                        {
                                            Name=ps.Name,
                                            Price = ps.Price
                                        })
                                        .OrderByDescending(sp=>sp.Price)
                                        .ToList()
                                        
                                    }
                })
                .ToList();

            var usersToExport = new ExportUsersDTO
            {
                Count = users.Count,
                Users = users.Take(10).ToList()
            };

            var serializer = new XmlSerializer(typeof(ExportUsersDTO), new XmlRootAttribute("Users"));

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, usersToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}