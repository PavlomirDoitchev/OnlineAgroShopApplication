using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroShopApp.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 9, int? categoryId = null, string? searchTerm = null)
        {
            if (User?.IsInRole("Admin") == true)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            var userId = GetUserId();
            var model = await _productService.GetPaginatedAsync(page, pageSize, categoryId, searchTerm, userId);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductGridPartial", model);
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var userId = this.GetUserId();

            var model = await _productService.GetDetailsAsync(id, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
   
    }
}
