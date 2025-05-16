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
        public int TotalProtein { get; set; }
        public int TotalCarbohydrates { get; set; }
        public int TotalFat { get; set; }
        public List<MealDTO> Meals { get; set; } = new();
    }
}
