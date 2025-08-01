using System.ComponentModel.DataAnnotations;
using AgroShopApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;


    public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = null!;

    public class InputModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessage = "Email and confirmation do not match.")]
        public string ConfirmEmail { get; set; } = null!;

        [Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string? CurrentPassword { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        Input = new InputModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ConfirmEmail = user.Email 
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        if (!ModelState.IsValid)
            return Page();

        if (string.IsNullOrWhiteSpace(Input.CurrentPassword))
        {
            ModelState.AddModelError("Input.CurrentPassword", "Current password is required to update your profile.");
            return Page();
        }

        var validPassword = await _userManager.CheckPasswordAsync(user, Input.CurrentPassword);
        if (!validPassword)
        {
            ModelState.AddModelError("Input.CurrentPassword", "Current password is incorrect.");
            return Page();
        }

        if (user.Email != Input.Email)
        {
            var existingUser = await _userManager.Users
                .Where(u => u.NormalizedEmail == Input.Email.ToUpper())
                .FirstOrDefaultAsync();
            if (existingUser != null && existingUser.Id != user.Id)
            {
                ModelState.AddModelError("Input.Email", "This email address is already in use.");
                return Page();
            }
            var emailResult = await _userManager.SetEmailAsync(user, Input.Email);
            if (!emailResult.Succeeded)
            {
                AddErrors(emailResult);
                return Page();
            }

            var usernameResult = await _userManager.SetUserNameAsync(user, Input.Email);
            if (!usernameResult.Succeeded)
            {
                AddErrors(usernameResult);
                return Page();
            }
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        user.FirstName = Input.FirstName;
        user.LastName = Input.LastName;

        var profileUpdateResult = await _userManager.UpdateAsync(user);
        if (!profileUpdateResult.Succeeded)
        {
            AddErrors(profileUpdateResult);
            return Page();
        }

        if (!string.IsNullOrWhiteSpace(Input.NewPassword))
        {
            var passwordResult = await _userManager.ChangePasswordAsync(user, Input.CurrentPassword, Input.NewPassword);
            if (!passwordResult.Succeeded)
            {
                AddErrors(passwordResult);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["Message"] = "Password and profile updated successfully.";
        }
        else
        {
            TempData["Message"] = "Profile updated successfully.";
        }

        return RedirectToPage();
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

}
