using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.Activity;
using Application.Interfaces.UseCases.ActivityType;
using Application.UseCases.ActivityType;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ActivityType
{
    public class GetActivitiesByNoticeIdTests
    {
        private readonly Mock<IActivityTypeRepository> _activityTypeRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetActivitiesByNoticeId CreateUseCase() =>
            new GetActivitiesByNoticeId(_activityTypeRepositoryMock.Object, _mapperMock.Object);

        private static Domain.Entities.ActivityType MockValidActivityType() => new("Type 1", "Unity 1", Guid.NewGuid());

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsListOfActivityTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var noticeId = Guid.NewGuid();
            var activityTypes = new List<Domain.Entities.ActivityType>
            {
                MockValidActivityType()
            };

            _activityTypeRepositoryMock.Setup(repo => repo.GetByNoticeIdAsync(noticeId)).ReturnsAsync(activityTypes);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ActivityTypeOutput>>(activityTypes)).Returns(new List<ActivityTypeOutput>());

            // Act
            var result = await useCase.ExecuteAsync(noticeId);

            // Assert
            Assert.NotNull(result);
            _activityTypeRepositoryMock.Verify(repo => repo.GetByNoticeIdAsync(noticeId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ActivityTypeOutput>>(activityTypes), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidId_ReturnsEmptyList()
        {
            // Arrange
            var useCase = CreateUseCase();
            var invalidNoticeId = Guid.Empty;

            // Act
            var result = await useCase.ExecuteAsync(invalidNoticeId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _activityTypeRepositoryMock.Verify(repo => repo.GetByNoticeIdAsync(invalidNoticeId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ActivityTypeOutput>>(It.IsAny<List<Domain.Entities.ActivityType>>()), Times.Never);
        }
    }
}
