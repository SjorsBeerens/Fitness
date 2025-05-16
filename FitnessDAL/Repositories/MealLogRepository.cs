using Microsoft.Data.SqlClient;
using FitnessDAL.DTO;

namespace FitnessDAL.Repositories
{
    public class MealLogRepository
    {
        private readonly string _connectionString;

        public MealLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MealLogDTO? GetMealLogByUserAndDate(int userId, DateOnly date)
        {
            MealLogDTO? mealLog = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        MealLog.MealLogID, 
                        MealLog.UserID, 
                        MealLog.Date, 
                        MealLog.Calories AS TotalCalories, 
                        MealLog.Protein AS TotalProtein, 
                        MealLog.Carbohydrates AS TotalCarbohydrates, 
                        MealLog.Fat AS TotalFat,
                        Meal.MealID, 
                        Meal.Name, 
                        Meal.Calories, 
                        Meal.Protein, 
                        Meal.Carbohydrates, 
                        Meal.Fat
                    FROM MealLog
                    LEFT JOIN Meal_MealLog ON MealLog.MealLogID = Meal_MealLog.MealLogID
                    LEFT JOIN Meal ON Meal_MealLog.MealID = Meal.MealID
                    WHERE MealLog.UserID = @UserID AND MealLog.Date = @Date";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (mealLog == null)
                            {
                                mealLog = new MealLogDTO
                                {
                                    MealLogID = reader.GetInt32(reader.GetOrdinal("MealLogID")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    Date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Date"))),
                                    TotalCalories = reader.GetInt32(reader.GetOrdinal("TotalCalories")),
                                    TotalProtein = reader.GetInt32(reader.GetOrdinal("TotalProtein")),
                                    TotalCarbohydrates = reader.GetInt32(reader.GetOrdinal("TotalCarbohydrates")),
                                    TotalFat = reader.GetInt32(reader.GetOrdinal("TotalFat")),
                                    Meals = new List<MealDTO>()
                                };
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("MealID")))
                            {
                                mealLog.Meals.Add(new MealDTO
                                {
                                    MealID = reader.GetInt32(reader.GetOrdinal("MealID")),
                                    MealName = reader["Name"].ToString() ?? "",
                                    calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                                    protein = reader.GetInt32(reader.GetOrdinal("Protein")),
                                    carbohydrates = reader.GetInt32(reader.GetOrdinal("Carbohydrates")),
                                    fat = reader.GetInt32(reader.GetOrdinal("Fat"))
                                });
                            }
                        }
                    }
                }
            }
            return mealLog;
        }
    }
}
