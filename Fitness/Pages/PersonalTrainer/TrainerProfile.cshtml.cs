using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessCore.Models;
using FitnessCore.Service;
using FitnessDAL.DTOs;
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

        public async Task OnGetAsync(string name)
        {
            // Haal de lijst van TrainerDTO's op
            var trainerDTOs = await _trainerRepository.GetTrainersAsync();

            // Map de DTO's naar de Trainer-modellen
            var trainers = _trainerService.MapToTrainers(trainerDTOs);

            // Zoek de juiste trainer
            Trainer = trainers.FirstOrDefault(t => t.Name == name);
        }
    }
}
