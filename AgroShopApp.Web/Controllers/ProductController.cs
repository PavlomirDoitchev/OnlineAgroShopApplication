using AgroShopApp.Data;
using AgroShopApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AgroShopApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly AgroShopDbContext _context;

        public ProductController(AgroShopDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? categoryId = null)
        {
            var productsQuery = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.IsAvailable);

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }

            var model = await productsQuery
                .Select(p => new AllProductsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                })
                .ToListAsync();

            return View(model);
        }
    }
}
