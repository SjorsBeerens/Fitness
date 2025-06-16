using FitnessDAL.DTO;
using FitnessDAL.Interfaces;

namespace FitnessCore.Services
{
    public class MealService
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
