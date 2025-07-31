using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
namespace AgroShopApp.Web.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, ICompositeViewEngine viewEngine, ILogger<OrdersController> logger)
            : base(viewEngine, logger)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var userId = GetUserId()!;
            var result = await _orderService.GetPaginatedUserOrdersAsync(userId.Value, page, pageSize);
            return SafeView("Index", result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var userId = GetUserId()!;
            var order = await _orderService.GetDetailsAsync(id, userId.Value);

            if (order == null)
            {
                return NotFound();
            }

            return SafeView("Details", order);
        }
    }
}