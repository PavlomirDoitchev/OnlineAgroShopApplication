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
        [HttpGet]
        public IActionResult Index(UserFilterInputModel filter, int page = 1, int pageSize = 10)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(u => u.Email.Contains(filter.Email));

            if (filter.IsDeleted.HasValue)
                query = query.Where(u => u.IsDeleted == filter.IsDeleted);

            var totalUsers = query.Count();
            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            var users = query
                .OrderBy(u => u.Email)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsDeleted = u.IsDeleted
                })
                .ToList();

            var model = new PaginatedUserListViewModel
            {
                Users = users,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };

            ViewBag.Filter = filter;

            return SafeView("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                TempData["Message"] = "Cannot delete an admin account.";
                return RedirectToAction("Index");
            }

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
            TempData["Message"] = "User deleted successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Restore(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            user.IsDeleted = false;
            await _userManager.UpdateAsync(user);

            TempData["Message"] = "User restored successfully.";
            return RedirectToAction("Index");
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

        }
           
    }

}
