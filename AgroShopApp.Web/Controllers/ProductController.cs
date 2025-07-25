using AgroShopApp.Data;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
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
            ViewBag.Categories = await _productService.GetCategoriesAsync();

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
        [Authorize]
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
        [Authorize]
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
        [Authorize] // TODO: change this to [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _productService.RemoveAsync(id);

            TempData["Message"] = "Product removed successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
