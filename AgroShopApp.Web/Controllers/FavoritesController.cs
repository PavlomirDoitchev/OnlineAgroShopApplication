using AgroShopApp.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace AgroShopApp.Web.Controllers
{
    public class FavoritesController : BaseController
    {
        private readonly IFavoritesService _favoriteService;

        public FavoritesController(IFavoritesService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
        {
            var userId = this.GetUserId();
            //var userId = _userManager.GetUserId(User);
            await _favoriteService.AddToFavoritesAsync(userId.Value, productId);

            TempData["Message"] = "Product added to favorites.";
            return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid productId, string? returnUrl = null)
        {
            var userId = this.GetUserId();
            await _favoriteService.RemoveFromFavoritesAsync(userId.Value, productId);

            TempData["Message"] = "Product removed from favorites.";
            return RedirectToAction("Index", "Favorites");
            //return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();
            var model = await _favoriteService.GetUserFavoritesAsync(userId.Value);

            return View(model);
        }
    }
}