namespace FitnessCore.Models
{
    public class TrainerBooking
    {
        public int BookingID { get; set; }
        public int TrainerID { get; set; }
        public Trainer Trainer { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
