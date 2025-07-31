using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : AdminBaseController
    {
        public HomeController(ICompositeViewEngine viewEngine, ILogger<HomeController> logger)
            : base(viewEngine, logger) { }

        public IActionResult Index()
        {
            return SafeView("Index");
        }
    }
}
