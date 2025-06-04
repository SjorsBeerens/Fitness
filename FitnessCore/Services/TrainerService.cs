using FitnessCore.Models;
using FitnessCore.Interfaces;
using FitnessDAL.DTOs;
using FitnessDAL.Interfaces;

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
                trainers.Add(dto.MapToTrainer());
            }
            return trainers;
        }
        public int GetRoundedRating(double rating)
        {
            return (int)Math.Floor(rating);
        }
    }
}
