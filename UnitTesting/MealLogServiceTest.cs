using System;
using FitnessCore.Services;
using FitnessDAL.Interfaces;
using FitnessDAL.DTO;
using Moq;
using Xunit;

namespace UnitTesting
{
    public class MealLogServiceTest
    {
        private readonly Mock<IMealLogRepository> _mealLogRepoMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly MealLogService _service;

        public MealLogServiceTest()
        {
            _service = new MealLogService(_mealLogRepoMock.Object, _userRepoMock.Object);
        }

        [Fact]
        public void EnsureMealLogForUserEmail_ReturnsExistingMealLogId_WhenMealLogExists()
        {
            // Arrange
            var userEmail = "test@example.com";
            var date = new DateOnly(2024, 6, 4);
            var existingMealLog = new MealLogDTO { MealLogID = 10 };
            _mealLogRepoMock.Setup(r => r.GetMealLogByUserEmailAndDate(userEmail, date)).Returns(existingMealLog);

            // Act
            var result = _service.EnsureMealLogForUserEmail(userEmail, date);

            // Assert
            Assert.Equal(10, result);
            _userRepoMock.Verify(r => r.GetUserIdByEmail(It.IsAny<string>()), Times.Never);
            _mealLogRepoMock.Verify(r => r.CreateMealLog(It.IsAny<int>(), It.IsAny<DateOnly>()), Times.Never);
        }

        [Fact]
        public void EnsureMealLogForUserEmail_CreatesMealLog_WhenNoneExists()
        {
            // Arrange
            var userEmail = "test@example.com";
            var date = new DateOnly(2024, 6, 4);
            _mealLogRepoMock.Setup(r => r.GetMealLogByUserEmailAndDate(userEmail, date)).Returns((MealLogDTO?)null);
            _userRepoMock.Setup(r => r.GetUserIdByEmail(userEmail)).Returns(5);
            _mealLogRepoMock.Setup(r => r.CreateMealLog(5, date)).Returns(99);

            // Act
            var result = _service.EnsureMealLogForUserEmail(userEmail, date);

            // Assert
            Assert.Equal(99, result);
            _userRepoMock.Verify(r => r.GetUserIdByEmail(userEmail), Times.Once);
            _mealLogRepoMock.Verify(r => r.CreateMealLog(5, date), Times.Once);
        }

        [Fact]
        public void EnsureMealLogForUserEmail_ThrowsException_WhenUserNotFound()
        {
            // Arrange
            var userEmail = "notfound@example.com";
            var date = new DateOnly(2024, 6, 4);
            _mealLogRepoMock.Setup(r => r.GetMealLogByUserEmailAndDate(userEmail, date)).Returns((MealLogDTO?)null);
            _userRepoMock.Setup(r => r.GetUserIdByEmail(userEmail)).Returns((int?)null);

            // Act & Assert
            Assert.Throws<Exception>(() => _service.EnsureMealLogForUserEmail(userEmail, date));
        }
    }
}
