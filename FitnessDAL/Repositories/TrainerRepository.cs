using Microsoft.Data.SqlClient;
using FitnessDAL.DTOs;

namespace FitnessCore.Repositories
{
    public class TrainerRepository
    {
        private readonly string _connectionString;

        public TrainerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<TrainerDTO>> GetTrainersAsync()
        {
            var trainers = new List<TrainerDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT TrainerID, Name, speciality AS Specialty, experience AS Experience, price AS Price, rating AS Rating FROM PersonalTrainer";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            trainers.Add(new TrainerDTO
                            {
                                TrainerID = Convert.ToInt32(reader["TrainerID"]),
                                Name = reader["Name"].ToString()!,
                                Specialty = reader["Specialty"].ToString()!,
                                Experience = reader["Experience"].ToString()!,
                                Price = reader["Price"].ToString()!,
                                Rating = Convert.ToDouble(reader["Rating"])
                            });
                        }
                    }
                }
            }

            return trainers;
        }
    }
}
