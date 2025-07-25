using AgroShopApp.Data;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AgroShopApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(IProductService productService, UserManager<IdentityUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }
       public async Task<IActionResult> Index(int? categoryId = null, string? searchTerm = null)
        {
            var userId = _userManager.GetUserId(User);

            var model = await _productService.GetAllAsync(categoryId, searchTerm, userId);

            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Categories = await _productService.GetCategoriesAsync(); // optional if you move this too

            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var userId = _userManager.GetUserId(User);

            var model = await _productService.GetDetailsAsync(id, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
