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
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var products = await _productService.GetAllAsync();
            int total = products.Count();
            int outOfStock = products.Count(p => p.StockQuantity == 0);
            var users = _userManager.Users.Where(u => !u.IsDeleted).ToList();

            var today = DateTime.Now.Date;

            var orders = await _orderService.GetFilteredOrdersAsync(new OrderFilterInputModel
            {
                FromDate = today,
                ToDate = today.AddDays(1).AddTicks(-1)
            });
            var orderStats = await _orderService.GetOrderStatsAsync(today.AddDays(-6), today.AddDays(1).AddTicks(-1));

            var topProducts = await _orderService.GetTopSellingProductsAsync(5); 
            var model = new AdminDashboardViewModel
            {
                TotalProducts = products.Count(),
                OutOfStock = products.Count(p => p.StockQuantity == 0),
                LowStock = products.Count(p => p.StockQuantity > 0 && p.StockQuantity <= 10),
                OrdersToday = orders.Count(o => o.Status != "Cancelled"),
                TodaysOrders = orders.OrderByDescending(o=>o.OrderedOn).Take(5).ToList(),
                RevenueLast7Days = orderStats,
                TopSellingProducts = topProducts
            };
            var last7Days = Enumerable.Range(0, 7)
                .Select(offset => DateTime.Now.Date.AddDays(-offset))
                .OrderBy(d => d)
                .ToList();

            var ordersPerDay = new List<OrdersPerDayViewModel>();

            foreach (var day in last7Days)
            {
                var dailyOrders = await _orderService.GetFilteredOrdersAsync(new OrderFilterInputModel
                {
                    FromDate = day,
                    ToDate = day.AddDays(1).AddTicks(-1)
                });

                var nonCancelledCount = dailyOrders.Count(o => o.Status != "Cancelled");

                ordersPerDay.Add(new OrdersPerDayViewModel
                {
                    DateLabel = day.ToString("MMM dd"),
                    Count = nonCancelledCount
                });
            }

            model.OrdersLast7Days = ordersPerDay;

            return SafeView("Index", model);
        }
    }
}
