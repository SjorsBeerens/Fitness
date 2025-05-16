using FitnessDAL.Repositories;
using FitnessDAL.DTO;
using System.Collections.Generic;

namespace FitnessCore.Services
{
    public class MealService
    {
        private readonly MealRepository _mealRepository;

        public MealService(MealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public List<MealDTO> GetAllMeals() => _mealRepository.GetAllMeals();
        public List<MealDTO> SearchMeals(string searchTerm) => _mealRepository.SearchMeals(searchTerm);
    }
}
