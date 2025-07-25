using AgroShopApp.Data;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AgroShopApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly AgroShopDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ProductController(AgroShopDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int? categoryId = null, string? searchTerm = null)
        {
            var productsQuery = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.IsAvailable);

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm));
            }

            var userId = _userManager.GetUserId(User);

            var userFavorites = userId != null
                ? await _context.Favorites
                    .Where(f => f.UserId == userId)
                    .Select(f => f.ProductId)
                    .ToListAsync()
                : new List<Guid>();

            var model = await productsQuery
                .Select(p => new AllProductsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    IsFavorite = userFavorites.Contains(p.Id)
                })
                .ToListAsync();

            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Categories = await _context.Categories
                .AsNoTracking()
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();

            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _context.Products
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == id && p.IsAvailable);

            if (product == null)
            {
                return NotFound();
            }

            var model = new AllProductsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category.Name,
                IsFavorite = User.Identity?.IsAuthenticated == true
                    && _context.Favorites.Any(f => f.ProductId == product.Id && f.UserId == _userManager.GetUserId(User))
            };

            return View(model);
        }
    }
}
