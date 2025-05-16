using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using FitnessCore.Interfaces;

namespace Fitness.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
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
