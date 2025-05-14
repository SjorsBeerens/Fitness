namespace FitnessCore.Models
{

    public class Trainer
    {
        public int TrainerID { get; set; }
        public required string Name { get; set; }
        public required string Specialty { get; set; }
        public required string Experience { get; set; }
        public required string Price { get; set; }
        public double Rating { get; set; }
        public List<TrainerSchedule> Schedules { get; set; }
        public List<TrainerBooking> Bookings { get; set; }

        public Trainer()
        {
            Schedules = new List<TrainerSchedule>();
            Bookings = new List<TrainerBooking>();
        }
    }
}