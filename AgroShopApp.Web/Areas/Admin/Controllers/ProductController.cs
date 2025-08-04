using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : AdminBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ICompositeViewEngine viewEngine, ILogger<ProductController> logger)
            : base(viewEngine, logger)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 9, int? categoryId = null, string? searchTerm = null)
        {
            var model = await _productService.GetPaginatedAsync(page, pageSize, categoryId, searchTerm);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductGridPartial", model);
            }

            return SafeView("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _productService.GetDetailsAsync(id, null);

            if (model == null)
            {
                return NotFound();
            }

            return SafeView("Details", model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _productService.GetCategoriesAsync();
            var model = new ProductFormViewModel { Categories = categories };
            return SafeView("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _productService.GetCategoriesAsync();
                return SafeView("Create", model);
            }

            await _productService.CreateAsync(model);
            TempData["Message"] = "Product added successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _productService.GetEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return SafeView("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _productService.GetCategoriesAsync();
                return SafeView("Edit", model);
            }

            await _productService.EditAsync(model);
            TempData["Message"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _productService.RemoveAsync(id);
            TempData["Message"] = "Product removed successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Deleted(int page = 1, int pageSize = 9, int? categoryId = null, string? searchTerm = null)
        {
            var model = await _productService.GetDeletedPaginatedAsync(page, pageSize, categoryId, searchTerm);

            return SafeView("Deleted", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            await _productService.RestoreAsync(id);
            TempData["Message"] = "Product restored successfully.";
            return RedirectToAction(nameof(Deleted));
        }
    }
}
