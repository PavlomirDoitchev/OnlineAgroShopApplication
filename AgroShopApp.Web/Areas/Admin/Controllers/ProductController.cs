using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 9, int? categoryId = null, string? searchTerm = null)
        {
            //var userId = GetUserId();
            var model = await _productService.GetPaginatedAsync(page, pageSize, categoryId, searchTerm);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductGridPartial", model);
            }

            return View(model);
        }
        [HttpGet]
        //[Route("Product/Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var model = await _productService.GetDetailsAsync(id, null);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _productService.GetCategoriesAsync();

            var model = new ProductFormViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _productService.GetCategoriesAsync();
                return View(model);
            }

            await _productService.CreateAsync(model);

            TempData["Message"] = "Product added successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _productService.RemoveAsync(id);

            TempData["Message"] = "Product removed successfully!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _productService.GetEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _productService.GetCategoriesAsync();
                return View(model);
            }

            await _productService.EditAsync(model);

            TempData["Message"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Deleted()
        {
            var model = await _productService.GetDeletedDetailedAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(Guid id)
        {
            await _productService.RestoreAsync(id);

            TempData["Message"] = "Product restored successfully.";
            return RedirectToAction(nameof(Deleted));
        }
    }
}
