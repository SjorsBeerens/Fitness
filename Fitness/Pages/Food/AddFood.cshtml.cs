using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessDAL.Repositories;
using FitnessDAL.DTO;
using System.Collections.Generic;

namespace Fitness.Pages.Food
{
    public class AddFoodModel : PageModel
    {
        private readonly MealService _mealService;

        public List<MealDTO> Meals { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public AddFoodModel(MealService mealService)
        {
            _mealService = mealService;
        }

        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
                Meals = _mealService.SearchMeals(SearchTerm);
            else
                Meals = _mealService.GetAllMeals();
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage(new { SearchTerm });
        }
    }
}
