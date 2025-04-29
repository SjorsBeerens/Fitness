using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Fitness.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            // Controleer of de sessie al bestaat
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Dashboard");
            }

            return Page();
        }

        [ValidateAntiForgeryToken] // Valideer anti-forgery token
        public IActionResult OnPost()
        {

            if (Email == "email@email.com" && Password == "password")
            {
                HttpContext.Session.SetString("UserEmail", Email);
                return RedirectToPage("/Dashboard");
            }

            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}
