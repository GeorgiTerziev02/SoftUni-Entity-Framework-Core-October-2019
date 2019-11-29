namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PetStore.Data;
    using PetStore.Data.Models;
    using PetStore.Services.Models.Brand;
    using Models.Toy;

    public class BrandService : IBrandService
    {
        private readonly PetStoreDbContext data;

        public BrandService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Create(string name)
        {
            if (name.Length > DataValidation.NameMaxLength)
            {
                throw new InvalidOperationException($"Brand name cannot be more than {DataValidation.NameMaxLength} characters.");
            }

            if (this.data.Brands.Any(br=>br.Name == name))
            {
                throw new InvalidOperationException($"Brand name {name} already exists.");
            }

            var brand = new Brand
            {
                Name = name
            };

            this.data.Brands.Add(brand);

            this.data.SaveChanges();

            return brand.Id;
        }

        public BrandWithToysServiceModel FindByIdWithToys(int id)
        {
            return this.data
                        .Brands
                        .Where(br => br.Id == id)
                        .Select(br => new BrandWithToysServiceModel
                        {
                            Name = br.Name,
                            Toys = br.Toys.Select(t=> new ToyListingServiceModel
                            {
                                Id = t.Id,
                                Name = t.Name,
                                Price = t.Price,
                                TotalOrders = t.Orders.Count
                            })
                        })
                .FirstOrDefault();
        }

        public IEnumerable<BrandListingServiceModel> SearchByName(string name)
        {
            var brands = this.data
                .Brands
                .Where(b=>b.Name.ToLower().Contains(name.ToLower()))
                .Select(b=> new BrandListingServiceModel
                { 
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();

            return brands;
        }
    }
}
