using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using AgroShopApp.Web.ViewModels.Cart;
namespace AgroShopApp.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
        {
            var userId = this.GetUserId();
            await _cartService.AddToCartAsync(userId!, productId);

            TempData["Message"] = "Product added to cart.";
            return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
        }
        [HttpGet]
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
        [ValidateAntiForgeryToken]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(Guid productId, int quantity)
        {
            var userId = GetUserId()!;

            if (quantity < 1)
            {
                TempData["Message"] = "Quantity must be at least 1.";
                return RedirectToAction(nameof(Index));
            }

            var stock = await _cartService.GetStockForProductAsync(productId);
            if (quantity > stock)
            {
                TempData["Message"] = $"Only {stock} item(s) in stock.";
                return RedirectToAction(nameof(Index));
            }

            await _cartService.SetQuantityAsync(userId, productId, quantity);

            TempData["Message"] = "Quantity updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var userId = GetUserId()!;
            await _cartService.RemoveFromCartAsync(userId, productId);

            TempData["Message"] = "Item removed from cart.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            var userId = GetUserId()!;
            var items = await _cartService.GetCartItemsAsync(userId);

            if (!items.Any())
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var total = items.Sum(i => i.Total);

            var model = new ConfirmOrderViewModel
            {
                Items = items.ToList(),
                TotalAmount = total
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = GetUserId()!;
            try
            {
                await _orderService.PlaceOrderAsync(userId);
                TempData["Message"] = "Order placed successfully!";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Confirm");
            }

            return RedirectToAction("Index", "Orders");
        }
    }
}