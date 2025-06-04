using FitnessDAL.Interfaces;
using FitnessDAL.DTO;
using System;

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
    }
}
