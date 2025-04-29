using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fitness.Pages.Login
{
    public class AdditionalInfoModel : PageModel
    {
        [BindProperty]
        public float Weight { get; set; }

        [BindProperty]
        public float Height { get; set; }

        [BindProperty]
        public int Age { get; set; }

        [BindProperty]
        public string Gender { get; set; }

        [BindProperty]
        public float ActivityLevel { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("/Dashboard");
        }
    }
}
