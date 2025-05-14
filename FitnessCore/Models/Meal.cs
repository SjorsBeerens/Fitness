namespace FitnessCore.Models
{
    public class Meal
    {
        public int MealID { get; set; }
        public string MealName { get; set; }
        public int calories { get; set; }
        public int protein { get; set; }
        public int carbohydrates { get; set; }
        public int fat { get; set; }

        public List<MealLog> MealLogs { get; set; }

        public Meal()
        {
            MealLogs = new List<MealLog>();
        }
    }
}
