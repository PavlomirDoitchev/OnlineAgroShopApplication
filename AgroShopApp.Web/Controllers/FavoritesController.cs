using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
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
            await _favoriteService.AddToFavoritesAsync(userId!, productId);

            TempData["Message"] = "Product added to favorites.";
            return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid productId, string? returnUrl = null)
        {
            var userId = this.GetUserId();
            await _favoriteService.RemoveFromFavoritesAsync(userId!, productId);

            TempData["Message"] = "Product removed from favorites.";
            return RedirectToAction("Index", "Favorites");
            //return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();
            var model = await _favoriteService.GetUserFavoritesAsync(userId!);

            return View(model);
        }
    }
}