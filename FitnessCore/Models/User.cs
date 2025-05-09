namespace FitnessCore.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal weight { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public double activityLevel { get; set; }
        public int height { get; set; }
    }
}