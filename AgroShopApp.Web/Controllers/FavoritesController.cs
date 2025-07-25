using AgroShopApp.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

public class FavoritesController : Controller
{
    private readonly IFavoritesService _favoriteService;
    private readonly UserManager<IdentityUser> _userManager;

    public FavoritesController(IFavoritesService favoriteService, UserManager<IdentityUser> userManager)
    {
        _favoriteService = favoriteService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
    {
        var userId = _userManager.GetUserId(User);
        await _favoriteService.AddToFavoritesAsync(userId!, productId);

        TempData["Message"] = "Product added to favorites.";
        return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Remove(Guid productId, string? returnUrl = null)
    {
        var userId = _userManager.GetUserId(User);
        await _favoriteService.RemoveFromFavoritesAsync(userId!, productId);

        TempData["Message"] = "Product removed from favorites.";
        return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var model = await _favoriteService.GetUserFavoritesAsync(userId!);

        return View(model);
    }
}