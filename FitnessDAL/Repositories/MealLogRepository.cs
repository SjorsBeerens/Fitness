using Microsoft.Data.SqlClient;
using FitnessDAL.DTO;
using FitnessDAL.Interfaces;

namespace FitnessDAL.Repositories
{
    public class MealLogRepository : IMealLogRepository
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
                                    TotalCalories = reader.GetInt32(reader.GetOrdinal("TotalCalories")), // Reverted to GetInt32
                                    TotalProtein = reader.GetDecimal(reader.GetOrdinal("TotalProtein")),
                                    TotalCarbohydrates = reader.GetDecimal(reader.GetOrdinal("TotalCarbohydrates")),
                                    TotalFat = reader.GetDecimal(reader.GetOrdinal("TotalFat")),
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
                                    protein = reader.GetDecimal(reader.GetOrdinal("Protein")),
                                    carbohydrates = reader.GetDecimal(reader.GetOrdinal("Carbohydrates")),
                                    fat = reader.GetDecimal(reader.GetOrdinal("Fat"))
                                });
                            }
                        }
                    }
                }
            }
            return mealLog;
        }



        public MealLogDTO? GetMealLogByUserEmailAndDate(string email, DateOnly date)
        {
            int? userId = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var userQuery = "SELECT UserID FROM [User] WHERE Email = @Email";
                using (var userCommand = new SqlCommand(userQuery, connection))
                {
                    userCommand.Parameters.AddWithValue("@Email", email);
                    var result = userCommand.ExecuteScalar();
                    if (result != null)
                        userId = Convert.ToInt32(result);
                }
            }
            if (userId == null)
                return null;

            return GetMealLogByUserAndDate(userId.Value, date);
        }

        public void AddMealToMealLog(int mealLogId, int mealId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"INSERT INTO Meal_MealLog (MealLogID, MealID) VALUES (@MealLogID, @MealID)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MealLogID", mealLogId);
                    command.Parameters.AddWithValue("@MealID", mealId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int CreateMealLog(int userId, DateOnly date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO MealLog (UserID, Date, Calories, Protein, Carbohydrates, Fat)
                    OUTPUT INSERTED.MealLogID
                    VALUES (@UserID, @Date, 0, 0, 0, 0)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void UpdateMealLogNutrientTotals(int mealLogId, int totalCalories, decimal totalProtein, decimal totalCarbohydrates, decimal totalFat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE MealLog
                    SET 
                        Calories = @TotalCalories,
                        Protein = @TotalProtein,
                        Carbohydrates = @TotalCarbohydrates,
                        Fat = @TotalFat
                    WHERE MealLogID = @MealLogID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MealLogID", mealLogId);
                    command.Parameters.AddWithValue("@TotalCalories", totalCalories); // totalCalories is now int
                    command.Parameters.AddWithValue("@TotalProtein", totalProtein);
                    command.Parameters.AddWithValue("@TotalCarbohydrates", totalCarbohydrates);
                    command.Parameters.AddWithValue("@TotalFat", totalFat);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int EnsureMealLogForUserEmail(string email, DateOnly date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    DECLARE @UserID INT;
                    SELECT @UserID = UserID FROM [User] WHERE Email = @Email;
                    IF @UserID IS NULL
                        THROW 50000, 'Gebruiker niet gevonden.', 1;

                    DECLARE @MealLogID INT;
                    SELECT @MealLogID = MealLogID FROM MealLog WHERE UserID = @UserID AND Date = @Date;

                    IF @MealLogID IS NULL
                    BEGIN
                        INSERT INTO MealLog (UserID, Date, Calories, Protein, Carbohydrates, Fat)
                        VALUES (@UserID, @Date, 0, 0, 0, 0);
                        SET @MealLogID = SCOPE_IDENTITY();
                    END

                    SELECT @MealLogID;
                ";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
    }
}
