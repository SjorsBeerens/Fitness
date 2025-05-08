using FitnessCore.Models;
using FitnessDAL.DTOs;
namespace FitnessCore.Service
{
    public class TrainerService
    {
        public List<Trainer> MapToTrainers(List<TrainerDTO> trainerDTOs)
        {
            return trainerDTOs.Select(dto => new Trainer
            {
                TrainerID = dto.TrainerID,
                Name = dto.Name,
                Specialty = dto.Specialty,
                Experience = dto.Experience,
                Price = dto.Price,
                Rating = dto.Rating
            }).ToList();
        }
    }
}