using System.Collections.Generic;
using FitnessDAL.DTO;

namespace FitnessDAL.Interfaces
{
    public interface IMealRepository
    {
        List<MealDTO> GetAllMeals();
        List<MealDTO> SearchMeals(string searchTerm);
    }
}
