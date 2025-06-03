namespace FitnessCore.Models
{
    public class Meal
    {
        public int MealID { get; set; }
        public string MealName { get; set; } = string.Empty;
        public int Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fat { get; set; }
    }
}
