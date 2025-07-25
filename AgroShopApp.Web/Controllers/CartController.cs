using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroShopApp.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        [HttpPost]
        public IActionResult Add(Guid productId)
        {
            TempData["Message"] = "Product added to cart (mock).";
            return RedirectToAction("Index", "Product");
        }
        public IActionResult Index()
        {
            ViewData["Message"] = "This is a mock cart page. Add-to-cart is not yet implemented.";
            return View();
        }
    }
}