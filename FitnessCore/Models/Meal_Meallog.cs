namespace FitnessCore.Models
{
    public class Meal_Meallog
    {
        public int MealID { get; set; }
        public Meal Meal { get; set; }

        public int MealLogID { get; set; }
        public MealLog MealLog { get; set; }
    }
}
