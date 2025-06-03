namespace FitnessCore.Interfaces
{
    public interface IUserService
    {
        bool IsEmailInUse(string email);
        int CreateUser(string fullName, string email, string password);
        (bool IsValid, int? UserId) ValidateUser(string email, string password);
        void UpdateUserAdditionalInfo(int userId, decimal weight, int height, int age, string gender, decimal activityLevel);
    }
}
