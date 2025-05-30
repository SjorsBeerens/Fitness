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

        public async Task<List<Trainer>> GetTrainersAsync()
        {
            var dtos = await _trainerRepository.GetTrainersAsync();
            return MapToTrainers(dtos);
        }

        public List<Trainer> MapToTrainers(List<TrainerDTO> dtos)
        {
            var trainers = new List<Trainer>();
            foreach (var dto in dtos)
            {
                if (dto == null) continue;
                trainers.Add(new Trainer
                {
                    TrainerID = dto.TrainerID,
                    Name = dto.Name ?? string.Empty,
                    Specialty = dto.Specialty ?? string.Empty,
                    Experience = dto.Experience ?? string.Empty,
                    Price = dto.Price ?? string.Empty,
                    Rating = dto.Rating,
                    Schedules = new List<TrainerSchedule>(),
                    Bookings = new List<TrainerBooking>()
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
