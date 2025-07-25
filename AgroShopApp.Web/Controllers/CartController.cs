using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

public class CartController : BaseController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
    {
        var userId = this.GetUserId();
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
