using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AgroShopApp.Data.Models;
using AgroShopApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace AgroShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager, ICompositeViewEngine viewEngine, ILogger<UsersController> logger)
            : base(viewEngine, logger)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email ?? string.Empty,
                    FirstName = u.FirstName
                })
                .ToList();

            return SafeView("Index", users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);

            TempData["Message"] = "User marked as deleted.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ResetEmail(Guid id, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            user.Email = newEmail;
            user.NormalizedEmail = newEmail.ToUpper();
            await _userManager.UpdateAsync(user);

            TempData["Message"] = "Email updated.";
            return RedirectToAction(nameof(Index));

            //public async Task<IActionResult> Details(Guid id)
            //{
            //    var user = await _userManager.FindByIdAsync(id.ToString());
            //    if (user == null)
            //        return NotFound();

            //    var model = new UserDetailsViewModel
            //    {
            //        Id = user.Id,
            //        Email = user.Email ?? string.Empty,
            //        Username = user.Username,
            //    };

            //    return View(model);
            //}
        }
    }

}
