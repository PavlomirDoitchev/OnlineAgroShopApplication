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
    public class FavoriteController : Controller
    {
        private readonly AgroShopDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoriteController(AgroShopDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            var userId = _userManager.GetUserId(User);

            var exists = _context.Favorites.Any(f => f.UserId == userId && f.ProductId == productId);
            if (!exists)
            {
                _context.Favorites.Add(new Favorite { UserId = userId!, ProductId = productId });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Product");
        }
        [Authorize]
        public async Task<IActionResult> Favorite()
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
    }
}
