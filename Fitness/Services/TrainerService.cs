using Fitness.Models;

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
                new Trainer { Name = "Emma Davis", Specialty = "Nutrition Coach", Experience = "4+ years", PriceRange = "$45-70/hour", Rating = 4.5, Availability = "Available Now" },
                new Trainer { Name = "John Doe", Specialty = "Barista", Experience = "100+ years", PriceRange = "$20/hour", Rating = 4, Availability = "Available Now"}
            };
        }

        public IEnumerable<Trainer> GetAllTrainers()
        {
            return GetTrainers();
        }
    }
}
