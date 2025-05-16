using System;

namespace FitnessDAL.DTO
{
    public class MealDTO
    {
        public int MealID { get; set; }
        public string MealName { get; set; }
        public int calories { get; set; }
        public decimal protein { get; set; }
        public decimal carbohydrates { get; set; }
        public decimal fat { get; set; }
    }
}
