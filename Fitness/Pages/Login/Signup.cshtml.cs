using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessCore.Services;
using FitnessCore.Interfaces;

namespace Fitness.Pages.Login
{
    public class SignupModel : PageModel
    {
        private readonly IUserService _userService;

        public SignupModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return Page();
            }

            if (_userService.IsEmailInUse(Email))
            {
                ModelState.AddModelError("Email", "This email address is already in use.");
                return Page();
            }

            var userId = _userService.CreateUser(FullName, Email, Password);
            TempData["UserID"] = userId;

            return RedirectToPage("AdditionalInfo");
        }
    }
}
