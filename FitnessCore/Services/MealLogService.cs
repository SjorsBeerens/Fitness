using FitnessCore.Models;
using FitnessDAL.Interfaces;
using System.Linq;

namespace FitnessCore.Services
{
    public class MealLogService
    {
        private readonly IMealLogRepository _mealLogRepository;
        private readonly IUserRepository _userRepository;

        public MealLogService(IMealLogRepository mealLogRepository, IUserRepository userRepository)
        {
            _mealLogRepository = mealLogRepository;
            _userRepository = userRepository;
        }

        public int EnsureMealLogForUserEmail(string userEmail, DateOnly date)
        {
            var mealLog = _mealLogRepository.GetMealLogByUserEmailAndDate(userEmail, date);
            if (mealLog != null)
                return mealLog.MealLogID;

            int? userId = _userRepository.GetUserIdByEmail(userEmail);
            if (userId == null)
                throw new Exception("Gebruiker niet gevonden.");

            return _mealLogRepository.CreateMealLog(userId.Value, date);
        }

        public void CalculateTotals(MealLog mealLog)
        {
            mealLog.TotalCalories = mealLog.Meals.Sum(m => m.Calories);
            mealLog.TotalProtein = mealLog.Meals.Sum(m => m.Protein);
            mealLog.TotalCarbohydrates = mealLog.Meals.Sum(m => m.Carbohydrates);
            mealLog.TotalFat = mealLog.Meals.Sum(m => m.Fat);
        }
    }
}
