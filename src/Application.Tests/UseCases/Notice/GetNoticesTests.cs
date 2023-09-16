using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;
using Moq;
using Xunit;
using Application.UseCases.Notice;

namespace Application.Tests.UseCases.Notice
{
    public class GetNoticesTests
    {
        private readonly Mock<INoticeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetNotices CreateUseCase() => new GetNotices(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.Notice MockValidNotice() => new(
            id: Guid.NewGuid(),
            registrationStartDate: DateTime.UtcNow,
            registrationEndDate: DateTime.UtcNow.AddDays(7),
            evaluationStartDate: DateTime.UtcNow.AddDays(8),
            evaluationEndDate: DateTime.UtcNow.AddDays(14),
            appealStartDate: DateTime.UtcNow.AddDays(15),
            appealFinalDate: DateTime.UtcNow.AddDays(21),
            sendingDocsStartDate: DateTime.UtcNow.AddDays(22),
            sendingDocsEndDate: DateTime.UtcNow.AddDays(28),
            partialReportDeadline: DateTime.UtcNow.AddDays(29),
            finalReportDeadline: DateTime.UtcNow.AddDays(35),
            description: "Edital de teste",
            suspensionYears: 1
        );

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsResumedReadNoticeOutputList()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = 10;
            var notices = new List<Domain.Entities.Notice>
            {
                MockValidNotice(),
                MockValidNotice(),
                MockValidNotice()
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(notices);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(notices)).Returns(new List<ResumedReadNoticeOutput>());

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetAllAsync(skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(notices), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_RepositoryReturnsEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = 10;
            var notices = new List<Domain.Entities.Notice>();

            _repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(notices);

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.Empty(result);
            _repositoryMock.Verify(repo => repo.GetAllAsync(skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(It.IsAny<IEnumerable<Domain.Entities.Notice>>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NegativeSkipValue_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = -1;
            var take = 10;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.ExecuteAsync(skip, take));
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(It.IsAny<IEnumerable<Domain.Entities.Notice>>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_NegativeTakeValue_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.ExecuteAsync(skip, take));
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(It.IsAny<IEnumerable<Domain.Entities.Notice>>()), Times.Never);
        }
    }
}
