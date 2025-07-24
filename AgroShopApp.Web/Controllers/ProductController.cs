using AgroShopApp.Data;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
