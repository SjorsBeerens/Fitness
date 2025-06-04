using FitnessCore.Services;
using FitnessDAL.DTOs;
using FitnessDAL.Interfaces;
using Moq;

namespace UnitTesting
{
    public class TrainerServiceTest
    {
        [Fact]
        public async Task GetTrainersAsync_ReturnsMappedTrainers()
        {
            // Arrange
            var trainerDtos = new List<TrainerDTO>
            {
                new TrainerDTO { TrainerID = 1, Name = "John", Specialty = "Strength", Experience = "5 years", Price = "50", Rating = 4.5 },
                new TrainerDTO { TrainerID = 2, Name = "Jane", Specialty = "Cardio", Experience = "3 years", Price = "40", Rating = 3.8 }
            };

            var mockRepo = new Mock<ITrainerRepository>();
            mockRepo.Setup(r => r.GetTrainersAsync()).ReturnsAsync(trainerDtos);

            var service = new TrainerService(mockRepo.Object);

            // Act
            var result = await service.GetTrainersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John", result[0].Name);
            Assert.Equal("Jane", result[1].Name);
        }

        [Fact]
        public void MapToTrainers_MapsAllProperties()
        {
            // Arrange
            var dtos = new List<TrainerDTO>
            {
                new TrainerDTO { TrainerID = 1, Name = "Test", Specialty = "Yoga", Experience = "2 years", Price = "30", Rating = 4.0 }
            };

            var service = new TrainerService(Mock.Of<ITrainerRepository>());

            // Act
            var trainers = service.MapToTrainers(dtos);

            // Assert
            Assert.Single(trainers);
            Assert.Equal("Test", trainers[0].Name);
            Assert.Equal("Yoga", trainers[0].Specialty);
            Assert.Equal(4.0, trainers[0].Rating);
        }

        [Theory]
        [InlineData(4.7, 4)]
        [InlineData(3.2, 3)]
        [InlineData(5.0, 5)]
        [InlineData(0.9, 0)]
        public void GetRoundedRating_ReturnsFlooredValue(double input, int expected)
        {
            // Arrange
            var service = new TrainerService(Mock.Of<ITrainerRepository>());

            // Act
            var result = service.GetRoundedRating(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
