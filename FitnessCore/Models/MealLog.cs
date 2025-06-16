using System;
using System.Collections.Generic;

namespace FitnessCore.Models
{
    public class MealLog
    {
        public int MealLogID { get; set; }
        public int UserID { get; set; }
        public DateOnly Date { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public int TotalCalories { get; set; }
        public decimal TotalProtein { get; set; }
        public decimal TotalCarbohydrates { get; set; }
        public decimal TotalFat { get; set; }
    }
}
