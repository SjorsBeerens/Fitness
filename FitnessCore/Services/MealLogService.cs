using FitnessDAL.Interfaces;
using Microsoft.Data.SqlClient;
using FitnessDAL.DTO;
using System;

namespace FitnessCore.Services
{
    public class MealLogService
    {
        private readonly IMealLogRepository _mealLogRepository;
        private readonly string _connectionString;

        public MealLogService(IMealLogRepository mealLogRepository, string connectionString)
        {
            _mealLogRepository = mealLogRepository;
            _connectionString = connectionString;
        }

        public int EnsureMealLogForUserEmail(string userEmail, DateOnly date)
        {
            var mealLog = _mealLogRepository.GetMealLogByUserEmailAndDate(userEmail, date);
            if (mealLog != null)
                return mealLog.MealLogID;

            int? userId = GetUserIdByEmail(userEmail);
            if (userId == null)
                throw new Exception("Gebruiker niet gevonden.");

            return _mealLogRepository.CreateMealLog(userId.Value, date);
        }

        private int? GetUserIdByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var userQuery = "SELECT UserID FROM [User] WHERE Email = @Email";
                using (var userCommand = new SqlCommand(userQuery, connection))
                {
                    userCommand.Parameters.AddWithValue("@Email", email);
                    var result = userCommand.ExecuteScalar();
                    if (result != null)
                        return Convert.ToInt32(result);
                }
            }
            return null;
        }
    }
}
