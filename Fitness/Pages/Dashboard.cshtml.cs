using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fitness.Pages
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Controleer of de sessie bestaat
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                // Als de sessie niet bestaat, redirect naar de loginpagina
                return RedirectToPage("/Login/login");
            }

            // Als de sessie bestaat, toon de dashboardpagina
            return Page();
        }
    }
}
