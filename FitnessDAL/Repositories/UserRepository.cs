using Microsoft.Data.SqlClient;
using FitnessDAL.Interfaces;

namespace FitnessDAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int? GetUserIdByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT UserID FROM [User] WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        return Convert.ToInt32(result);
                }
            }
            return null;
        }


        public bool IsEmailInUse(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(1) FROM [User] WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }

        public int CreateUser(string fullName, string email, string hashedPassword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                INSERT INTO [User] (Name, Email, Password)
                OUTPUT INSERTED.UserID
                VALUES (@Name, @Email, @Password)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", fullName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public (int? UserId, string? HashedPassword) GetUserByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT UserID, Password FROM [User] WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ((int)reader["UserID"], reader["Password"].ToString());
                        }
                    }
                }
            }
            return (null, null);
        }
        public void UpdateUserAdditionalInfo(int userId, decimal weight, int height, int age, string gender, decimal activityLevel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
            UPDATE [User]
            SET Weight = @Weight,
                Height = @Height,
                Age = @Age,
                Gender = @Gender,
                ActivityLevel = @ActivityLevel
            WHERE UserID = @UserID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@Height", height);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@ActivityLevel", activityLevel);
                    command.Parameters.AddWithValue("@UserID", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
