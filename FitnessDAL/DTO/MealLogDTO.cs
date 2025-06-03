using System;
using System.Collections.Generic;

namespace FitnessDAL.DTO
{
    public class MealLogDTO
    {
        public int MealLogID { get; set; }
        public int UserID { get; set; }
        public DateOnly Date { get; set; }
        public int TotalCalories { get; set; }
        public decimal TotalProtein { get; set; }
        public decimal TotalCarbohydrates { get; set; }
        public decimal TotalFat { get; set; }
        public List<MealDTO> Meals { get; set; } = new();
    }
}
