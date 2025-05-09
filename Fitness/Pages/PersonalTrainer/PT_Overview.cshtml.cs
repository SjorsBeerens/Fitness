using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessCore.Repositories;
using FitnessCore.Models;
using FitnessCore.Service;

namespace Fitness.Pages.PersonalTrainer
{
    public class PT_OverviewModel : PageModel
    {
        private readonly TrainerRepository _trainerRepository;
        private readonly TrainerService _trainerService;

        public PT_OverviewModel(TrainerRepository trainerRepository, TrainerService trainerService)
        {
            _trainerRepository = trainerRepository;
            _trainerService = trainerService;
        }

        public List<Trainer> Trainers { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login/login");
            }

            var trainerDTOs = await _trainerRepository.GetTrainersAsync();
            Trainers = _trainerService.MapToTrainers(trainerDTOs);

            return Page();
        }
    }
}
