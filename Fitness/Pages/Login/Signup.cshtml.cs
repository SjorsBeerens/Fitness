using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 50 characters.")]
        public string FullName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Password must be alphanumeric.")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
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
