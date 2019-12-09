namespace PetStore.Services.Implementations
{
    using System;
    using Data.Models;
    using Services.Models.Food;

    public class FoodService : IFoodService
    {
        public void BuyFromDistributor(string name, double weight, decimal price, double profit, DateTime expirationDate, string brandName, string categoryName)
        {
            

            var food = new Food()
            {
                Name = name,
                Weight = weight,
                DistributorPrice = price,
                Price = price + (price * (decimal)profit),
                ExpirationDate = expirationDate
            };
        }

        public void BuyFromDistributor(AddingFoodServiceModel model)
        {
            throw new NotImplementedException();
        }
    }
}
