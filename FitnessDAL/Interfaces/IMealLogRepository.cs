using FitnessDAL.DTO;

namespace FitnessDAL.Interfaces
{
    public interface IMealLogRepository
    {
        MealLogDTO? GetMealLogByUserAndDate(int userId, DateOnly date);
        MealLogDTO? GetMealLogByUserEmailAndDate(string email, DateOnly date);
        void AddMealToMealLog(int mealLogId, int mealId);
        int CreateMealLog(int userId, DateOnly date);
    }
}
