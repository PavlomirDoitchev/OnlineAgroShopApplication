using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using AgroShopApp.Data.Models;


namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : AdminBaseController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            IProductService productService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            ICompositeViewEngine viewEngine,
            ILogger<HomeController> logger)
            : base(viewEngine, logger)
        {
            _productService = productService;
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var products = await _productService.GetAllAsync();
            int total = products.Count();
            int outOfStock = products.Count(p => p.StockQuantity == 0);
            var users = _userManager.Users.Where(u => !u.IsDeleted).ToList();

            var today = DateTime.UtcNow.Date;

            var orders = await _orderService.GetFilteredOrdersAsync(new OrderFilterInputModel
            {
                FromDate = today,
                ToDate = today.AddDays(1).AddTicks(-1)
            });

            var model = new AdminDashboardViewModel
            {
                TotalProducts = products.Count(),
                OutOfStock = products.Count(p => p.StockQuantity == 0),
                TotalUsers = users.Count,
                OrdersToday = orders.Count(),
                TodaysOrders = orders.ToList()
            };
            var last7Days = Enumerable.Range(0, 7)
                .Select(offset => DateTime.UtcNow.Date.AddDays(-offset))
                .OrderBy(d => d)
                .ToList();

            var ordersPerDay = new List<OrdersPerDayViewModel>();

            foreach (var day in last7Days)
            {
                var count = (await _orderService.GetFilteredOrdersAsync(new OrderFilterInputModel
                {
                    FromDate = day,
                    ToDate = day.AddDays(1).AddTicks(-1)
                })).Count();

                ordersPerDay.Add(new OrdersPerDayViewModel
                {
                    DateLabel = day.ToString("MMM dd"),
                    Count = count
                });
            }

            model.OrdersLast7Days = ordersPerDay;

            return SafeView("Index", model);
        }
    }
}
