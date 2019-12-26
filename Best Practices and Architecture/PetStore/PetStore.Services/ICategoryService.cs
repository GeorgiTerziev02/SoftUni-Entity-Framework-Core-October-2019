namespace PetStore.Services
{
    using PetStore.Services.Models.Category;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        DetailsCategoryServiceModel GetById(int id);

        void Create(CreateCategoryServiceModel model);

        void Edit(EditCategoryServiceModel model);

        bool Delete(int id);

        bool Exists(int categoryId);

        IEnumerable<CategoryListingServiceModel> All();
    }
}
