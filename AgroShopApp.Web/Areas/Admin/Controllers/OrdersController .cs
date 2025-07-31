using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
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
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _orderService.GetOrderDetailsAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid orderId, string status)
        {
            bool updated = await _orderService.UpdateStatusAsync(orderId, status);

            if (!updated)
            {
                TempData["Message"] = "Failed to update order status.";
                return RedirectToAction(nameof(Details), new { id = orderId });
            }

            TempData["Message"] = "Order status updated successfully.";
            return RedirectToAction(nameof(Details), new { id = orderId });
        }
    }
}
