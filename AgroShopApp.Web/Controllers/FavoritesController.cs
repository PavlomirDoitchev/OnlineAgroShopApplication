using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
namespace AgroShopApp.Web.Controllers
{
    [UserOnly]
    public class FavoritesController : BaseController
    {
        private readonly IFavoritesService _favoriteService;

        public FavoritesController(IFavoritesService favoriteService, ICompositeViewEngine viewEngine, ILogger<FavoritesController> logger)
            : base(viewEngine, logger)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
        {
            var userId = this.GetUserId();
            await _favoriteService.AddToFavoritesAsync(userId.Value, productId);

            TempData["Message"] = "Product added to favorites.";
            return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid productId, string? returnUrl = null)
        {
            var userId = this.GetUserId();
            await _favoriteService.RemoveFromFavoritesAsync(userId.Value, productId);

            TempData["Message"] = "Product removed from favorites.";
            return RedirectToAction("Index", "Favorites");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();
            var model = await _favoriteService.GetUserFavoritesAsync(userId.Value);
            return SafeView("Index", model);
        }
    }
}