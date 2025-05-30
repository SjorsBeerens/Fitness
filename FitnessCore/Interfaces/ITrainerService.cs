using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessDAL.DTOs;
using FitnessCore.Models;

namespace FitnessCore.Interfaces
{
    public interface ITrainerService
    {
        Task<List<Trainer>> GetTrainersAsync();
        List<Trainer> MapToTrainers(List<TrainerDTO> dtos);
    }
}
