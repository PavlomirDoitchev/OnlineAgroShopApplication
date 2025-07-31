namespace AspNetCoreArchTemplate.Web.Controllers
{
    using System.Diagnostics;

    using ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AgroShopApp.Web.Controllers;
    using Microsoft.AspNetCore.Mvc.ViewEngines;

    public class HomeController : BaseController
    {
        public HomeController(ICompositeViewEngine viewEngine, ILogger<HomeController> logger)
            : base(viewEngine, logger) { }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return SafeView("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return SafeView("Contact");
        }

        public IActionResult Privacy()
        {
            return SafeView("Privacy");
        }

        [HttpGet("/Home/Error")]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode == 404)
            {
                return SafeView("NotFoundError");
            }

            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return SafeView("Error", model);
        }

        [HttpGet("/Home/UnauthorizedError")]
        [AllowAnonymous]
        public IActionResult UnauthorizedError()
        {
            return SafeView("UnauthorizedError");
        }
    }
}
