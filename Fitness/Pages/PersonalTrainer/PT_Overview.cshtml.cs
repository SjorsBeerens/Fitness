using Microsoft.AspNetCore.Mvc.RazorPages;
using Fitness.Repositories;

namespace Fitness.Pages.PersonalTrainer
{
    public class PT_OverviewModel : PageModel
    {
        private readonly TrainerRepository _trainerRepository;

        public PT_OverviewModel(TrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public List<Trainer> Trainers { get; private set; } = new();

        public async Task OnGetAsync()
        {
            Trainers = await _trainerRepository.GetTrainersAsync();
        }
    }
}
