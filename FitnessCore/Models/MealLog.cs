using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessCore.Models
{
    public class MealLog
    {
        public int MealLogID { get; set; }
        public int UserID { get; set; }
        public DateOnly Date { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();

        // Calculated totals
        public int TotalCalories { get; private set; }
        public decimal TotalProtein { get; private set; }
        public decimal TotalCarbohydrates { get; private set; }
        public decimal TotalFat { get; private set; }

        public void CalculateTotals()
        {
            TotalCalories = Meals.Sum(m => m.Calories);
            TotalProtein = Meals.Sum(m => m.Protein);
            TotalCarbohydrates = Meals.Sum(m => m.Carbohydrates);
            TotalFat = Meals.Sum(m => m.Fat);
        }
    }
}
