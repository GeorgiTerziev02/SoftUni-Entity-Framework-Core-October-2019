namespace PetStore.Services
{
    using System;

    using PetStore.Services.Models.Food;

    public interface IFoodService
    {
        void BuyFromDistributor(string name, double weight, decimal price, double profit,
            DateTime expirationDate, string brandName, string categoryName);

        void BuyFromDistributor(AddingFoodServiceModel model);
    }
}
