using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using AgroShopApp.Web.ViewModels.Cart;
using AgroShopApp.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using AgroShopApp.Web.Infrastructure.Filters;
namespace AgroShopApp.Web.Controllers
{
    [UserOnly]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;


        public CartController(ICartService cartService, IOrderService orderService, ICompositeViewEngine viewEngine, ILogger<UsersController> logger)
            :base(viewEngine, logger)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId()!;
            var cartItems = await _cartService.GetCartItemsAsync(userId.Value);
            return SafeView("Index", cartItems);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Guid productId, string? returnUrl = null)
        {
            var userId = this.GetUserId();
            await _cartService.AddToCartAsync(userId.Value, productId);

            TempData["Message"] = "Product added to cart.";
            return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
        }
        [HttpPost]
        public async Task<IActionResult> Decrease(Guid productId)
        {
            var userId = GetUserId()!;
            await _cartService.DecreaseQuantityAsync(userId.Value, productId);

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
                await _cartService.AddToCartAsync(userId.Value, productId);
                TempData["Message"] = "Product quantity updated.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody] QuantityUpdateInputModel? model)
        {
            if (model == null)
                return BadRequest("Invalid input.");

            var userId = GetUserId()!;

            if (model.Quantity < 1)
                model.Quantity = 1;

            var stock = await _cartService.GetStockForProductAsync(model.ProductId);
            if (model.Quantity > stock)
                model.Quantity = stock;

            await _cartService.SetQuantityAsync(userId.Value, model.ProductId, model.Quantity);

            var price = await _cartService.GetProductPriceAsync(model.ProductId);
            var itemTotal = price * model.Quantity;
            var grandTotal = await _cartService.GetCartTotalAsync(userId.Value);

            return Ok(new
            {
                correctedQuantity = model.Quantity,
                itemTotal,
                grandTotal
            });
        }
        [HttpPost]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var userId = GetUserId()!;
            await _cartService.RemoveFromCartAsync(userId.Value, productId);

            TempData["Message"] = "Item removed from cart.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            var userId = GetUserId()!;
            var items = await _cartService.GetCartItemsAsync(userId.Value);

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

            return SafeView("Confirm", model);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = GetUserId()!;
            try
            {
                await _orderService.PlaceOrderAsync(userId.Value);
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