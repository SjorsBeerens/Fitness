using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using FitnessCore.Interfaces;

namespace Fitness.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Password must be alphanumeric.")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (isValid, userId) = _userService.ValidateUser(Email, Password);
            if (isValid && userId.HasValue)
            {
                HttpContext.Session.SetString("UserEmail", Email);
                HttpContext.Session.SetInt32("UserID", userId.Value);

                return RedirectToPage("/Dashboard");
            }

            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}
