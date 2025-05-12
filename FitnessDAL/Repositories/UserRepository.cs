using Microsoft.Data.SqlClient;

namespace FitnessDAL.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
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
    }
}
