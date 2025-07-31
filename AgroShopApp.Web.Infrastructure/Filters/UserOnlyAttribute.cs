using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AgroShopApp.Web.Infrastructure.Filters
{
    public class UserOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity?.IsAuthenticated == true && user.IsInRole("Admin"))
            {
                // Access TempData via service provider
                var tempDataFactory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
                var tempData = tempDataFactory?.GetTempData(context.HttpContext);

                if (tempData != null)
                {
                    tempData["Message"] = "Access denied: This page is for regular users only.";
                }

                context.Result = new RedirectToActionResult("Index", "Home", new { area = "Admin" });
            }

            base.OnActionExecuting(context);
        }
    }
}
