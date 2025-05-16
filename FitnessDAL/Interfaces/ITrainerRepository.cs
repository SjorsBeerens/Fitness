using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessDAL.DTOs;

namespace FitnessDAL.Interfaces
{
    public interface ITrainerRepository
    {
        Task<List<TrainerDTO>> GetTrainersAsync();
    }
}
