namespace Fitness.Models
{
    public class Trainer
    {
        public required string Name { get; set; }
        public required string Specialty { get; set; }
        public required string Experience { get; set; }
        public required string PriceRange { get; set; }
        public double Rating { get; set; }
        public required string Availability { get; set; }
    }
}
