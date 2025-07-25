using AgroShopApp.Data;
using AgroShopApp.Data.Models;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly AgroShopDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoritesController(AgroShopDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Product)
                .ThenInclude(p => p.Category)
                .Select(f => new FavoriteProductViewModel
                {
                    ProductId = f.Product.Id,
                    Name = f.Product.Name,
                    Description = f.Product.Description,
                    Price = f.Product.Price,
                    ImageUrl = f.Product.ImageUrl,
                    Category = f.Product.Category.Name
                })
                .ToListAsync();

            return View(favorites);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
        {
            var userId = _userManager.GetUserId(User);

            if (!_context.Favorites.Any(f => f.UserId == userId && f.ProductId == productId))
            {
                _context.Favorites.Add(new Favorite { UserId = userId!, ProductId = productId });
                await _context.SaveChangesAsync();
                TempData["Message"] = "Product added to favorites.";
            }

            return !string.IsNullOrEmpty(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index", "Product");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(Guid productId, string? returnUrl = null)
        {
            var userId = _userManager.GetUserId(User);

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Product removed from favorites.";
            }

            return !string.IsNullOrEmpty(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index", "Product");
        }
    }
}
