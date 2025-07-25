using AgroShopApp.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ICartService cartService, UserManager<IdentityUser> userManager)
    {
        _cartService = cartService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
    {
        var userId = _userManager.GetUserId(User);
        await _cartService.AddToCartAsync(userId!, productId);

        TempData["Message"] = "Product added to cart.";
        return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
    }

    public IActionResult Index()
    {
        ViewData["Message"] = "Cart system coming soon!";
        return View();
    }
}
