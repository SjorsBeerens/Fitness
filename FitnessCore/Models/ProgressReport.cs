namespace FitnessCore.Models
{
    public class ProgressReport
    {
        public int ProgressReportID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

        public string WeightProgress { get; set; }
        public string BMIProgress { get; set; }
    }
}
