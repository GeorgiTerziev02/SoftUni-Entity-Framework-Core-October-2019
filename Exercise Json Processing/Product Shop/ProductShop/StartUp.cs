using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Dto;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var db = new ProductShopContext())
            {
                //var inputJson = File.ReadAllText("./../../../Datasets/categories-products.json");

                var result = GetUsersWithProducts(db);

                Console.WriteLine(result);
            }
        }

        //Problem 01
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        //Problem 02
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        //Problem 03
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson).Where(c => c.Name != null).ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        //Problem 04
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //Problem 05
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .ToList();

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var result = JsonConvert.SerializeObject(products, new JsonSerializerSettings()
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented
            });

            return result;
        }

        //Problem 06
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Where(ps => ps.Buyer != null).Count() >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    SoldProducts = u.ProductsSold.Select(ps => new
                    {
                        ps.Name,
                        ps.Price,
                        BuyerFirstName = ps.Buyer.FirstName,
                        BuyerLastName = ps.Buyer.LastName
                    })
                })
                .ToList();

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented

            });

            return json;
        }

        //Problem 07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .OrderByDescending(c => c.CategoryProducts.Count())
                //.Where(c => c.Name != null)
                .Select(c => new
                {
                    Category = c.Name,
                    ProductsCount = c.CategoryProducts.Count(),
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price).ToString("f2"),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price).ToString("f2")
                })
                .ToList();

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };


            var json = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented
            });


            return json;
        }

        //Problem 08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            //var usersWithProducts = context
            //    .Users
            //    .Where(u => u.ProductsSold.Where(ps => ps.Buyer != null).Count() >= 1)
            //    .OrderByDescending(u => u.ProductsSold.Where(ps => ps.Buyer != null).Count())
            //    .Select(u => new
            //    {
            //        u.FirstName,
            //        u.LastName,
            //        u.Age,
            //        SoldProducts = new 
            //        { 
            //            Count = u.ProductsSold.Where(ps => ps.Buyer != null).Count(),
            //            Products = u.ProductsSold
            //                    .Where(ps => ps.Buyer != null)
            //                    .Select(ps => new
            //                    {
            //                        ps.Name,
            //                        ps.Price
            //                    }).ToList()
            //        }
            //    })
            //    .ToList();
            
            //var usersOuput = new
            //{
            //    UsersCount = usersWithProducts.Count,
            //    Users = usersWithProducts
            //};

            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .ProjectTo<UserDetailsDto>()
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToArray();

            var usersOuput = new UserInfoDto()
            {
                UsersCount = users.Length,
                Users = users
            };

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var export = JsonConvert.SerializeObject(usersOuput, new JsonSerializerSettings
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return export;
        }
    }
}