using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessCore.Models;
using FitnessCore.Services;

namespace Fitness.Pages.PersonalTrainer
{
    public class TrainerProfileModel : PageModel
    {
        private readonly TrainerService _trainerService;

        public TrainerProfileModel(TrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public Trainer? Trainer { get; private set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login/login");
            }

            var trainers = await _trainerService.GetTrainersAsync();
            Trainer = trainers.FirstOrDefault(t => t.Name == name);

            return Page();
        }
    }
}
