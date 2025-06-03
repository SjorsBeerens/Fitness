using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using FitnessDAL.DTO;
using FitnessDAL.Interfaces;
using System.Threading.Tasks;

namespace FitnessDAL.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly string _connectionString;

        public MealRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<MealDTO> GetAllMeals()
        {
            var meals = new List<MealDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"SELECT MealID, Name, Calories, Protein, Carbohydrates, Fat FROM Meal";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        meals.Add(new MealDTO
                        {
                            MealID = reader.GetInt32(reader.GetOrdinal("MealID")),
                            MealName = reader["Name"].ToString() ?? "",
                            calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                            protein = Convert.ToDecimal(reader["Protein"]),
                            carbohydrates = Convert.ToDecimal(reader["Carbohydrates"]),
                            fat = Convert.ToDecimal(reader["Fat"])
                        });
                    }
                }
            }
            return meals;
        }

        public List<MealDTO> SearchMeals(string searchTerm)
        {
            var meals = new List<MealDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"SELECT MealID, Name, Calories, Protein, Carbohydrates, Fat FROM Meal WHERE Name LIKE @Search";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            meals.Add(new MealDTO
                            {
                                MealID = reader.GetInt32(reader.GetOrdinal("MealID")),
                                MealName = reader["Name"].ToString() ?? "",
                                calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                                protein = Convert.ToDecimal(reader["Protein"]),
                                carbohydrates = Convert.ToDecimal(reader["Carbohydrates"]),
                                fat = Convert.ToDecimal(reader["Fat"])
                            });
                        }
                    }
                }
            }
            return meals;
        }

        public async Task<IEnumerable<MealDTO>> SearchMealsAsync(string searchTerm)
        {
            var meals = new List<MealDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT MealID, Name, Calories, Protein, Carbohydrates, Fat FROM Meal WHERE Name LIKE @SearchTerm";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            meals.Add(new MealDTO
                            {
                                MealID = reader.GetInt32(reader.GetOrdinal("MealID")),
                                MealName = reader.GetString(reader.GetOrdinal("Name")),
                                calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                                protein = reader.GetDecimal(reader.GetOrdinal("Protein")),
                                carbohydrates = reader.GetDecimal(reader.GetOrdinal("Carbohydrates")),
                                fat = reader.GetDecimal(reader.GetOrdinal("Fat"))
                            });
                        }
                    }
                }
            }
            return meals;
        }

        public async Task<MealDTO?> GetMealByIdAsync(int mealId)
        {
            MealDTO? meal = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT MealID, Name, Calories, Protein, Carbohydrates, Fat FROM Meal WHERE MealID = @MealID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MealID", mealId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            meal = new MealDTO
                            {
                                MealID = reader.GetInt32(reader.GetOrdinal("MealID")),
                                MealName = reader.GetString(reader.GetOrdinal("Name")),
                                calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                                protein = reader.GetDecimal(reader.GetOrdinal("Protein")),
                                carbohydrates = reader.GetDecimal(reader.GetOrdinal("Carbohydrates")),
                                fat = reader.GetDecimal(reader.GetOrdinal("Fat"))
                            };
                        }
                    }
                }
            }
            return meal;
        }
    }
}
