namespace PetStore.Services
{
    using PetStore.Services.Models.Category;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        DetailsCategoryServiceModel GetById(int id);

        void Create(CreateCategoryServiceModel model);

        bool Exists(int categoryId);

        IEnumerable<CategoryListingServiceModel> All();
    }
}
