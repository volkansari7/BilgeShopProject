using BilgeShop.Business.Dto;
using BilgeShop.Business.Services;
using BilgeShop.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult List()
        {
            var categoryDtoList = _categoryService.GetCategories();

            var viewModel = categoryDtoList.Select(x => new CategoryListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult New()
        {
            return View("Form", new CategoryFormViewModel());
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var categoryDto = _categoryService.GetCategory(id);

            var viewModel = new CategoryFormViewModel()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            return View("Form", viewModel);
        }
        [HttpPost]
        public IActionResult Save(CategoryFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", formData);
            }

            if (formData.Id == 0) // Ekleme işlemi
            {
                var addCategoryDto = new AddCategotyDto()
                {
                    Name = formData.Name.Trim(),
                    Description = formData.Description,
                };

                var result = _categoryService.AddCategory(addCategoryDto);

                if (result)
                {
                    RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu isimde zaten bir kategori mevcut";
                    return View("Form", formData);
                }
            }
            else // Güncelleme İşlemi 
            {
                var updateCategoryDto = new UpdateCategoryDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData.Description
                };

                _categoryService.UpdateCategory(updateCategoryDto);

                return RedirectToAction("List");
            }

            return RedirectToAction("List");

        }

        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
}
