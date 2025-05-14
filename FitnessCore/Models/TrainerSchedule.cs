namespace FitnessCore.Models
{
    public class TrainerSchedule
    {
        public int ScheduleID { get; set; }
        public int TrainerID { get; set; }
        public Trainer Trainer { get; set; }

        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public bool IsBooked { get; set; }
    }
}
