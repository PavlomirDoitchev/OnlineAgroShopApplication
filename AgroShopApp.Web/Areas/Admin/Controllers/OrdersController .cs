using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : AdminBaseController
    {
        private readonly IOrderService _orderService;
        private static readonly HashSet<string> AllowedStatuses = new() { "Pending", "Completed", "Cancelled" };
        public OrdersController(IOrderService orderService, ICompositeViewEngine viewEngine, ILogger<OrdersController> logger)
            : base(viewEngine, logger)
        {
            _orderService = orderService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index(OrderFilterInputModel filter)
        //{
        //    var orders = await _orderService.GetFilteredOrdersAsync(filter);
        //    ViewBag.Filter = filter; 
        //    return View(orders);
        //}
        [HttpGet]
        public async Task<IActionResult> Index(OrderFilterInputModel filter, int page = 1, int pageSize = 10)
        {
            var viewModel = await _orderService.GetPaginatedFilteredOrdersAsync(filter, page, pageSize);
            return SafeView("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _orderService.GetOrderDetailsAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return SafeView("Details", order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid orderId, string status)
        {
            if (!AllowedStatuses.Contains(status))
            {
                TempData["Message"] = "Invalid status selected.";
                return RedirectToAction(nameof(Details), new { id = orderId });
            }

            bool updated = await _orderService.UpdateStatusAsync(orderId, status);

            TempData["Message"] = updated
                ? "Order status updated successfully."
                : "Failed to update order status.";

            return RedirectToAction(nameof(Details), new { id = orderId });
        }
    }
}
