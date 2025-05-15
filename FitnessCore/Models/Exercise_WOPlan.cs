namespace FitnessCore.Models
{
    public class Exercise_WOPlan
    {
        public int ExerciseID { get; set; }
        public Exercise Exercise { get; set; }

        public int PlanID { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }
    }
}
