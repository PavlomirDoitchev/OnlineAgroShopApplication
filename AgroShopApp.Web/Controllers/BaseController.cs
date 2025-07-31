using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AgroShopApp.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ILogger _logger;

        protected BaseController(ICompositeViewEngine viewEngine, ILogger logger)
        {
            _viewEngine = viewEngine;
            _logger = logger;
        }

        protected bool IsUserAuthenticated()
        {
            return this.User.Identity?.IsAuthenticated ?? false;
        }

        protected Guid? GetUserId()
        {
            if (!IsUserAuthenticated())
                return null;

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userIdClaim, out Guid guid))
            {
                return guid;
            }

            return null;
        }

        protected IActionResult SafeView(string viewName)
        {
            var result = _viewEngine.FindView(ControllerContext, viewName, false);

            if (!result.Success)
            {
                _logger.LogWarning("View '{ViewName}' not found. Returning fallback view.", viewName);
                return View("~/Views/Shared/GenericError.cshtml");
            }

            return View(viewName);
        }
        protected IActionResult SafeView(string viewName, object? model = null)
        {
            var result = _viewEngine.FindView(ControllerContext, viewName, false);
            if (!result.Success)
            {
                _logger.LogWarning("View '{ViewName}' not found. Returning fallback.", viewName);
                return View("~/Views/Shared/GenericError.cshtml");
            }

            return View(viewName, model);
        }
    }
}
