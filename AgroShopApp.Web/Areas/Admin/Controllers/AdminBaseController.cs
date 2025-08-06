using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static AgroShopApp.GCommon.UserRoles;
namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area(AppAdmin)]
    [Authorize(Roles = AppAdmin)]
    public abstract class AdminBaseController : Controller
    {
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ILogger _logger;

        protected AdminBaseController(ICompositeViewEngine viewEngine, ILogger logger)
        {
            _viewEngine = viewEngine;
            _logger = logger;
        }

        protected IActionResult SafeView(string viewName, object? model = null)
        {
            var result = _viewEngine.FindView(ControllerContext, viewName, false);
            if (!result.Success)
            {
                _logger.LogWarning("Admin View '{ViewName}' not found. Fallback triggered.", viewName);
                return View("~/Views/Shared/GenericError.cshtml");
            }

            return View(viewName, model);
        }
    }
}
