using Microsoft.AspNetCore.Mvc;

namespace AgroShopApp.Web.Controllers
{
    public class CartController : Controller
    {
        [HttpPost]
        public IActionResult Add(Guid productId)
        {
            TempData["Message"] = "Product added to cart (mock).";
            return RedirectToAction("Index", "Product");
        }

    }
}