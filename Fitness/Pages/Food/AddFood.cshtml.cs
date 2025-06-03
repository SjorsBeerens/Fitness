using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessDAL.DTO;
using FitnessCore.Interfaces;
using FitnessDAL.Interfaces;
using FitnessCore.Services;
using System.Collections.Generic;

namespace Fitness.Pages.Food
{
    public class AddFoodModel : PageModel
    {
        private readonly IMealService _mealService;
        private readonly IMealLogRepository _mealLogRepository;
        private readonly MealLogService _mealLogService;

        public List<MealDTO> Meals { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public AddFoodModel(
            IMealService mealService,
            IMealLogRepository mealLogRepository,
            MealLogService mealLogService)
        {
            _mealService = mealService;
            _mealLogRepository = mealLogRepository;
            _mealLogService = mealLogService;
        }

        public void OnGet()
        {
            Meals = !string.IsNullOrWhiteSpace(SearchTerm)
                ? _mealService.SearchMeals(SearchTerm)
                : _mealService.GetAllMeals();
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage(new { SearchTerm });
        }

        public IActionResult OnPostAddMealAsync(int mealId)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login");
            }

            var date = DateOnly.FromDateTime(DateTime.Today);

            int mealLogId;
            try
            {
                mealLogId = _mealLogService.EnsureMealLogForUserEmail(userEmail, date);
            }
            catch
            {
                ModelState.AddModelError("", "Gebruiker niet gevonden.");
                return Page();
            }

            _mealLogRepository.AddMealToMealLog(mealLogId, mealId);

            return RedirectToPage("/Food/FoodOverview");
        }
    }
}
