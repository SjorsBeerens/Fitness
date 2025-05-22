using System.Collections.Generic;
using FitnessDAL.DTO;

namespace FitnessCore.Interfaces
{
    public interface IMealService
    {
        List<MealDTO> GetAllMeals();
        List<MealDTO> SearchMeals(string searchTerm);
    }
}
