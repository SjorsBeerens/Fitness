using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessCore.Interfaces;
using FitnessCore.Models;
using FitnessDAL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.Pages.PersonalTrainer
{
    public class PT_OverviewModel : PageModel
    {
        private readonly ITrainerService _trainerService;

        public PT_OverviewModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public List<Trainer> Trainers { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login/login");
            }

            var trainerDTOs = await _trainerService.GetTrainersAsync();
            Trainers = _trainerService.MapToTrainers(trainerDTOs);

            return Page();
        }
    }
}