namespace Fitness.Services
{
    public class TrainerService
    {
        public List<Trainer> GetTrainers()
        {
            return new List<Trainer>
            {
                new Trainer { Name = "Max Verstappen", Specialty = "Strength Training", Experience = "5+ years", PriceRange = "$2000/hour", Rating = 5, Availability = "Available Now" },
                new Trainer { Name = "Mike Thompson", Specialty = "Cardio Specialist", Experience = "3+ years", PriceRange = "$40-60/hour", Rating = 4.5, Availability = "Not Available" },
                new Trainer { Name = "Emma Davis", Specialty = "Nutrition Coach", Experience = "4+ years", PriceRange = "$45-70/hour", Rating = 4.5, Availability = "Available Now" }
            };
        }

        public IEnumerable<Trainer> GetAllTrainers()
        {
            return GetTrainers();
        }
    }

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
