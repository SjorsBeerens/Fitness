using FitnessDAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace FitnessCore.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsEmailInUse(string email)
        {
            return _userRepository.IsEmailInUse(email);
        }

        public int CreateUser(string fullName, string email, string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, password);
            return _userRepository.CreateUser(fullName, email, hashedPassword);
        }

        public (bool IsValid, int? UserId) ValidateUser(string email, string password)
        {
            var (userId, hashedPassword) = _userRepository.GetUserByEmail(email);
            if (userId.HasValue)
            {
                var passwordHasher = new PasswordHasher<object>();
                var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
                if (result == PasswordVerificationResult.Success)
                {
                    return (true, userId);
                }
            }
            return (false, null);
        }
    }
}
