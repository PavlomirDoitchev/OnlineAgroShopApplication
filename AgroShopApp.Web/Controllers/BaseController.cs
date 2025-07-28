using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgroShopApp.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
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
    }
}
