namespace FitnessDAL.Interfaces
{
    public interface IUserRepository
    {
        bool IsEmailInUse(string email);
        int CreateUser(string fullName, string email, string hashedPassword);
        (int? UserId, string? HashedPassword) GetUserByEmail(string email);
    }
}
