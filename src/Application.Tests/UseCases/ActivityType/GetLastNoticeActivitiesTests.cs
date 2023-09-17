using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.Activity;
using Application.Interfaces.UseCases.ActivityType;
using Application.UseCases.ActivityType;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases.ActivityType
{
    public class GetLastNoticeActivitiesTests
    {
        private readonly Mock<IActivityTypeRepository> _activityTypeRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetLastNoticeActivities CreateUseCase() =>
            new GetLastNoticeActivities(_activityTypeRepositoryMock.Object, _mapperMock.Object);

        private static Domain.Entities.ActivityType MockValidActivityType() => new("Type 1", "Unity 1", Guid.NewGuid());

        [Fact]
        public async Task ExecuteAsync_ValidData_ReturnsListOfActivityTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var activityTypes = new List<Domain.Entities.ActivityType>
            {
                MockValidActivityType()
            };

            _activityTypeRepositoryMock.Setup(repo => repo.GetLastNoticeActivitiesAsync()).ReturnsAsync(activityTypes);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ActivityTypeOutput>>(activityTypes)).Returns(new List<ActivityTypeOutput>());

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            _activityTypeRepositoryMock.Verify(repo => repo.GetLastNoticeActivitiesAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ActivityTypeOutput>>(activityTypes), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NoData_ReturnsEmptyList()
        {
            // Arrange
            var useCase = CreateUseCase();
            List<Domain.Entities.ActivityType> activityTypes = null;

            _activityTypeRepositoryMock.Setup(repo => repo.GetLastNoticeActivitiesAsync()).ReturnsAsync(activityTypes);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _activityTypeRepositoryMock.Verify(repo => repo.GetLastNoticeActivitiesAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ActivityTypeOutput>>(It.IsAny<List<Domain.Entities.ActivityType>>()), Times.Never);
        }
    }
}
