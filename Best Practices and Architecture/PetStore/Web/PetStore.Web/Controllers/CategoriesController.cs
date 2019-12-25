namespace PetStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetStore.Services;
    using PetStore.Services.Models.Category;
    using PetStore.Web.Models.Categories;
    using System.Linq;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var categoryServiceModel = new CreateCategoryServiceModel()
            {
                Name = model.Name,
                Description = model.Description
            };

            this.categoryService.Create(categoryServiceModel);

            return this.RedirectToAction("All", "Categories");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = categoryService.GetById(id);

            if (category.Name == null)
            {
                return BadRequest();
            }

            CategoryDetailsViewModel viewModel = new CategoryDetailsViewModel()
            {
                Id = category.Id.Value,
                Name = category.Name,
                Description = category.Description
            };

            if (viewModel.Description == null)
            {
                viewModel.Description = "No description";
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = this.categoryService.GetById(id);

            if (category.Name == null)
            {
                return this.BadRequest();
            }

            var viewModel = new CategoryDetailsViewModel()
            {
                Id = category.Id.Value,
                Name = category.Name,
                Description = category.Description
            };

            if (viewModel.Description == null)
            {
                viewModel.Description = "No description.";
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return null;
        }

        public IActionResult All()
        {
            var categories = categoryService.All()
                .Select(csm => new CategoryListingViewModel()
                {
                    Id = csm.Id,
                    Name = csm.Name
                })
                .ToArray();

            return View(categories);
        }
    }
}