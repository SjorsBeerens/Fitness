namespace Fitness.Models
{
    public class MealLog
    {
        public int MealLogID { get; set; }
        public int UserID { get; set; }
        public DateOnly Date { get; set; }
        public int TotalCalories { get; set; }
        public int TotalProtein { get; set; }
        public int TotalCarbohydrates { get; set; }
        public int TotalFat { get; set; }
    }
}
