using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId()!;
        var cartItems = await _cartService.GetCartItemsAsync(userId);
        return View(cartItems);
    }
    [HttpPost]
    public async Task<IActionResult> Decrease(Guid productId)
    {
        var userId = GetUserId()!;
        await _cartService.DecreaseQuantityAsync(userId, productId);

        TempData["Message"] = "Product quantity updated.";
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Increase(Guid productId)
    {
        var userId = GetUserId()!;
        try
        {
            await _cartService.AddToCartAsync(userId, productId);
            TempData["Message"] = "Product quantity updated.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Message"] = ex.Message;
        }

        return RedirectToAction("Index");
    }
}
