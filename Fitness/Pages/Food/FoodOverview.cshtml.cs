using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessDAL.Repositories;
using FitnessDAL.DTO;

namespace Fitness.Pages.Food
{
    public class FoodOverviewModel : PageModel
    {
        private readonly MealLogRepository _mealLogRepository;

        public MealLogDTO? MealLog { get; set; }

        public FoodOverviewModel(MealLogRepository mealLogRepository)
        {
            _mealLogRepository = mealLogRepository;
        }

        public void OnGet()
        {
            int userId = 1; // Haal de userId op uit authenticatie/session indien mogelijk
            var date = DateOnly.FromDateTime(DateTime.Today); // Of haal de datum uit querystring/form indien gewenst
            MealLog = _mealLogRepository.GetMealLogByUserAndDate(userId, date);
        }
    }
}
