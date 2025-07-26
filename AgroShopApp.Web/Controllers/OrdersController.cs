using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
namespace AgroShopApp.Web.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var userId = GetUserId()!;
            var result = await _orderService.GetPaginatedUserOrdersAsync(userId, page, pageSize);

            return View(result);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var userId = GetUserId()!;
            var order = await _orderService.GetDetailsAsync(id, userId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}