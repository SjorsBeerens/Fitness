using FitnessCore.Interfaces;
using FitnessCore.Models;
using FitnessDAL.DTO;
using FitnessDAL.Interfaces;
using System.Collections.Generic;

namespace FitnessCore.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public List<MealDTO> GetAllMeals() => _mealRepository.GetAllMeals();
        public List<MealDTO> SearchMeals(string searchTerm) => _mealRepository.SearchMeals(searchTerm);
    }
}
