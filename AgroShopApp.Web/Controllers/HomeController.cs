namespace AspNetCoreArchTemplate.Web.Controllers
{
    using System.Diagnostics;

    using ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AgroShopApp.Web.Controllers;

    public class HomeController : BaseController
    {
        //TODO: Finish this implementation
        private readonly Dictionary<int, string> HttpStatusCodeViewMap = new Dictionary<int, string>()
        {
            {401, "UnauthorizedError"},
            {403, "UnauthorizedError"},
            {404, "NotFoundError"},
            {500, "ServerError"}
        };
        public HomeController(ILogger<HomeController> logger)
        {

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            //return StatusCode(403);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode)
        {
            switch (statusCode)
            {
                case 401:
                case 403:
                    return View("UnauthorizedError");
                case 404:
                    return View("NotFoundError");
                default:
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
