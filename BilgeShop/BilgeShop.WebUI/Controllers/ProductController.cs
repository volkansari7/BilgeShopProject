using BilgeShop.Business.Services;
using BilgeShop.WebUI.Areas.Admin.Models;
using BilgeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Detail(int id)
        {
            var productDtos = _productService.GetProductById(id);

            var viewModel = new ProductViewModel()
            {
                Id = productDtos.Id,
                Name = productDtos.Name,
                Description = productDtos.Description,
                UnitsInStock = productDtos.UnitsInStock,
                UnitPrice = productDtos.UnitPrice,
                ImagePath = productDtos.ImagePath,
                CategoryId = productDtos.CategoryId,
                CategoryName = productDtos.CategoryName,
            };

            return View(viewModel);
        }
    }
}
