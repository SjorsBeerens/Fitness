namespace FitnessCore.Models
{
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string ExerciseName { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public decimal weight { get; set; }
        public List<Exercise_WOPlan> WorkoutPlans { get; set; }

        public Exercise()
        {
            WorkoutPlans = new List<Exercise_WOPlan>();
        }
    }
}
