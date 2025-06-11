using FitnessCore.Services;
using FitnessDAL.DTO;
using FitnessDAL.Interfaces;
using Moq;

namespace UnitTesting
{
    public class MealServiceTest
    {
        private readonly Mock<IMealRepository> _mealRepoMock = new();
        private readonly MealService _service;

        public MealServiceTest()
        {
            _service = new MealService(_mealRepoMock.Object);
        }

        [Fact]
        public void GetAllMeals_ReturnsAllMeals()
        {
            // Arrange
            var meals = new List<MealDTO>
            {
                new MealDTO { MealID = 1, MealName = "Salad" },
                new MealDTO { MealID = 2, MealName = "Pasta" }
            };
            _mealRepoMock.Setup(r => r.GetAllMeals()).Returns(meals);

            // Act
            var result = _service.GetAllMeals();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Salad", result[0].MealName);
            Assert.Equal("Pasta", result[1].MealName);
        }

        [Fact]
        public void SearchMeals_ReturnsMatchingMeals()
        {
            // Arrange
            var searchTerm = "Salad";
            var meals = new List<MealDTO>
            {
                new MealDTO { MealID = 1, MealName = "Salad" }
            };
            _mealRepoMock.Setup(r => r.SearchMeals(searchTerm)).Returns(meals);

            // Act
            var result = _service.SearchMeals(searchTerm);

            // Assert
            Assert.Single(result);
            Assert.Equal("Salad", result[0].MealName);
        }
    }
}
