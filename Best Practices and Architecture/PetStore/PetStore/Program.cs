namespace PetStore
{
    using PetStore.Data;
    using PetStore.Services.Implementations;
    using System;

    public class Program
    {
        public static void Main()
        {
            using (var data = new PetStoreDbContext())
            {
                var brandService = new BrandService(data);

                var brandWithToys = brandService.FindByIdWithToys(1);
            }
        }
    }
}
