namespace PetStore.Services.Implementations
{
    using PetStore.Data;
    using PetStore.Data.Models;
    using PetStore.Services.Models.Category;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly PetStoreDbContext data;

        public CategoryService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public DetailsCategoryServiceModel GetById(int id)
        {
            var category = this.data
                .Categories
                .Find(id);

            var model = new DetailsCategoryServiceModel()
            { 
                Id = category?.Id,
                Name = category?.Name,
                Description = category?.Description
            };

            return model;
        }

        public void Create(CreateCategoryServiceModel model)
        {
            var category = new Category()
            {
                Name = model.Name,
                Description = model.Description
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();
        }

        public void Edit(EditCategoryServiceModel model)
        {
            var category = this.data.Categories.Find(model.Id);

            category.Name = model.Name;
            category.Description = model.Description;

            this.data.SaveChanges();
        }

        public bool Exists(int categoryId)
        {
            return this.data.Categories.Any(c => c.Id == categoryId);
        }

        public IEnumerable<CategoryListingServiceModel> All()
        {
            return this.data
                .Categories
                .Select(c => new CategoryListingServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToArray();
        }

        public bool Delete(int id)
        {
            var category = this.data
                .Categories
                .Find(id);

            if (category == null)
            {
                return false;
            }

            this.data.Categories.Remove(category);
            int deletedEntitiesCount = this.data.SaveChanges();

            if (deletedEntitiesCount == 0)
            {
                return false;
            }

            return true;
        }
    }
}
