using Microsoft.AspNetCore.Mvc.RazorPages;
using Fitness.Services;

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

        public void OnGet(string name)
        {
            Trainer = _trainerService.GetAllTrainers().FirstOrDefault(t => t.Name == name);
        }
    }
}
