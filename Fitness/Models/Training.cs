namespace Fitness.Models
{
    public class Training
    {
        public int TrainingID { get; set; }
        public int TrainerID { get; set; }
        public int UserID { get; set; }
        public int PlanID { get; set; }
        public DateOnly Date { get; set; }
        
    }
}
