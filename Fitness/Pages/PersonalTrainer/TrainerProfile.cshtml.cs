using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessCore.Models;
using FitnessCore.Services;
using FitnessCore.Repositories;

namespace Fitness.Pages.PersonalTrainer
{
    public class TrainerProfileModel : PageModel
    {
        private readonly TrainerRepository _trainerRepository;
        private readonly TrainerService _trainerService;

        public TrainerProfileModel(TrainerRepository trainerRepository, TrainerService trainerService)
        {
            _trainerRepository = trainerRepository;
            _trainerService = trainerService;
        }

        public Trainer? Trainer { get; private set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login/login");
            }

            var trainerDTOs = await _trainerRepository.GetTrainersAsync();
            var trainers = _trainerService.MapToTrainers(trainerDTOs);
            Trainer = trainers.FirstOrDefault(t => t.Name == name);

            return Page();
        }
    }
}
