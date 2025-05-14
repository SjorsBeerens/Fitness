namespace FitnessCore.Models
{
    public class WorkoutPlan
    {
        public int PlanID { get; set; }
        public int UserID { get; set; }
        public int TrainingID { get; set; }

        public string PlanName { get; set; }
        public string PlanDescription { get; set; }

        public List<Exercise_WOPlan> Exercises { get; set; }

        public WorkoutPlan()
        {
            Exercises = new List<Exercise_WOPlan>();
        }
    }
}
