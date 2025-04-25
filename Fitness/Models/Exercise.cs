namespace Fitness.Models
{
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string ExerciseName { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public decimal weight { get; set; }

    }
}
