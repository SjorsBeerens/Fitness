using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessDAL.DTO;
using FitnessDAL.Interfaces;

namespace Fitness.Pages.Food
{
    public class FoodOverviewModel : PageModel
    {
        private readonly IMealLogRepository _mealLogRepository;

        public MealLogDTO? MealLog { get; set; }

        public FoodOverviewModel(IMealLogRepository mealLogRepository)
        {
            _mealLogRepository = mealLogRepository;
        }

        public void OnGet()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                Response.Redirect("/Login");
                return;
            }
            var date = DateOnly.FromDateTime(DateTime.Today);
            MealLog = _mealLogRepository.GetMealLogByUserEmailAndDate(userEmail, date);
        }
    }
}
