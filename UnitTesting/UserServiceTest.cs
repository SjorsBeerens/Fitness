using Moq;
using FitnessCore.Services;
using FitnessDAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace UnitTesting
{
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepoMock = new();

        public UserServiceTest()
        {
            _userService = new UserService(_userRepoMock.Object);
        }

        [Fact]
        public void IsEmailInUse_ReturnsTrue_WhenEmailExists()
        {
            // Arrange
            var email = "test@example.com";
            _userRepoMock.Setup(r => r.IsEmailInUse(email)).Returns(true);

            // Act
            var result = _userService.IsEmailInUse(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmailInUse_ReturnsFalse_WhenEmailDoesNotExist()
        {
            // Arrange
            var email = "notfound@example.com";
            _userRepoMock.Setup(r => r.IsEmailInUse(email)).Returns(false);

            // Act
            var result = _userService.IsEmailInUse(email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CreateUser_ReturnsUserId()
        {
            // Arrange
            var fullName = "Test User";
            var email = "test@example.com";
            var password = "password";
            var expectedUserId = 42;
            _userRepoMock.Setup(r => r.CreateUser(fullName, email, It.IsAny<string>())).Returns(expectedUserId);

            // Act
            var result = _userService.CreateUser(fullName, email, password);

            // Assert
            Assert.Equal(expectedUserId, result);
        }

        [Fact]
        public void ValidateUser_ReturnsValid_WhenCredentialsMatch()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var userId = 1;
            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, password);
            _userRepoMock.Setup(r => r.GetUserByEmail(email)).Returns((userId, hashedPassword));

            // Act
            var result = _userService.ValidateUser(email, password);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public void ValidateUser_ReturnsInvalid_WhenCredentialsDoNotMatch()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var userId = 1;
            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, "not_the_password");
            _userRepoMock.Setup(r => r.GetUserByEmail(email)).Returns((userId, hashedPassword));

            // Act
            var result = _userService.ValidateUser(email, password);

            // Assert
            Assert.False(result.IsValid);
            Assert.Null(result.UserId);
        }

        [Fact]
        public void UpdateUserAdditionalInfo_CallsRepository()
        {
            // Arrange
            int userId = 1;
            decimal weight = 70;
            int height = 180;
            int age = 30;
            string gender = "M";
            decimal activityLevel = 1.2m;

            // Act
            _userService.UpdateUserAdditionalInfo(userId, weight, height, age, gender, activityLevel);

            // Assert
            _userRepoMock.Verify(r => r.UpdateUserAdditionalInfo(userId, weight, height, age, gender, activityLevel), Times.Once);
        }
    }
}