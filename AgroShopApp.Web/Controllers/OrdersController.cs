using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using AgroShopApp.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static AgroShopApp.GCommon.ApplicationConstants.TempDataMessages;
namespace AgroShopApp.Web.Controllers
{
    [UserOnly]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var success = await _orderService.TryCancelOrderAsync(id, User);

            if (!success)
            {
                TempData["Message"] = OrderCouldNotBeCancelled;
            }
            else
            {
                TempData["Message"] = OrderCancelled;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}