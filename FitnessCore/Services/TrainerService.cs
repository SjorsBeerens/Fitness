using FitnessCore.Models;
using FitnessCore.Interfaces;
using FitnessDAL.DTOs;
using FitnessDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessCore.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;

        public TrainerService(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public Task<List<TrainerDTO>> GetTrainersAsync() => _trainerRepository.GetTrainersAsync();

        public List<Trainer> MapToTrainers(List<TrainerDTO> dtos)
        {
            var trainers = new List<Trainer>();
            foreach (var dto in dtos)
            {
                trainers.Add(new Trainer
                {
                    TrainerID = dto.TrainerID,
                    Name = dto.Name ?? "",
                    Specialty = dto.Specialty ?? "",
                    Experience = dto.Experience ?? "",
                    Price = dto.Price ?? "",
                    Rating = dto.Rating
                });
            }
            return trainers;
        }

        public int GetRoundedRating(double rating)
        {
            return (int)Math.Floor(rating);
        }
    }
}
