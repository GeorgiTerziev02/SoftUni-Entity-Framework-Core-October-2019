namespace PetStore
{
    using PetStore.Data;
    using PetStore.Data.Models;
    using PetStore.Services.Implementations;
    using System;

    public class Program
    {
        public static void Main()
        {

            using (var data = new PetStoreDbContext())
            {
                var userService = new UserService(data);
                //var toyService = new ToyService(data, userService);
                var breedService = new BreedService(data);
                var categoryService = new CategoryService(data);
                var petService = new PetService(data, breedService, categoryService, userService);

                petService.BuyPet(Gender.Male, DateTime.Now, 0m, null, 1, 1);

                petService.SellPet(1, 1);
            }
        }
    }
}
