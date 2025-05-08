namespace FitnessCore.Models
{
    public class WorkoutPlan
    {
        public int PlanID { get; set; }
        public int UserID { get; set; }
        public int TrainingID { get; set; }

        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
    }
}
