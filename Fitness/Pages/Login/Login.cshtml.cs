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
                // Als de sessie bestaat, redirect naar dashboard
                return RedirectToPage("/Dashboard");
            }

            return Page();
        }

        [ValidateAntiForgeryToken] // Valideer anti-forgery token
        public IActionResult OnPost()
        {
            // Dummy authenticatie (vervang dit met je eigen logica)
            if (Email == "email@email.com" && Password == "password")
            {
                // Sla gebruikersinformatie op in de sessie
                HttpContext.Session.SetString("UserEmail", Email);

                // Redirect naar een beveiligde pagina
                return RedirectToPage("/Dashboard");
            }

            // Toon foutmelding als authenticatie mislukt
            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}
