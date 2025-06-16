using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessCore.Services;
using FitnessCore.Models;
using FitnessDAL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.Pages.PersonalTrainer
{
    public class PT_OverviewModel : PageModel
    {
        private readonly TrainerService _trainerService;

        public PT_OverviewModel(TrainerService trainerService)
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

            Trainers = await _trainerService.GetTrainersAsync();

            return Page();
        }
    }
}