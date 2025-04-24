using Microsoft.AspNetCore.Mvc.RazorPages;
using Fitness.Repositories;

namespace Fitness.Pages.PersonalTrainer
{
    public class TrainerProfileModel : PageModel
    {
        private readonly TrainerRepository _trainerRepository;

        public TrainerProfileModel(TrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public Trainer? Trainer { get; private set; }

        public async Task OnGetAsync(string name)
        {
            var trainers = await _trainerRepository.GetTrainersAsync();
            Trainer = trainers.FirstOrDefault(t => t.Name == name);
        }
    }
}
