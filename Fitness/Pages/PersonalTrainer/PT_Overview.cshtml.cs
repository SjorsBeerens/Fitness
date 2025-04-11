using Microsoft.AspNetCore.Mvc.RazorPages;
using Fitness.Services;

namespace Fitness.Pages.PersonalTrainer
{
    public class PT_OverviewModel : PageModel
    {
        private readonly TrainerService _trainerService;

        public PT_OverviewModel(TrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public IEnumerable<Trainer>? Trainers { get; private set; } // Mark as nullable

        public void OnGet()
        {
            Trainers = _trainerService.GetAllTrainers();
        }
    }
}
