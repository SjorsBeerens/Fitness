namespace FitnessCore.Models
{
    public class User_PT
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int TrainerID { get; set; }
        public Trainer Trainer { get; set; }
    }
}
