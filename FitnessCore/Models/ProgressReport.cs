namespace FitnessCore.Models
{
    public class ProgressReport
    {
        public int ProgressReportID { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; } // Mark as nullable

        public double WeightProgress { get; set; }
        public double BMIProgress { get; set; }
        public DateTime Date { get; set; } // Add Date property
    }
}
