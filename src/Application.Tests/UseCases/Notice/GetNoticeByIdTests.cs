using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.Notice;

namespace Application.Tests.UseCases.Notice
{
    public class GetNoticeByIdTests
    {
        private readonly Mock<INoticeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetNoticeById CreateUseCase() => new GetNoticeById(_repositoryMock.Object, _mapperMock.Object);
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
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadNoticeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var noticeToRetrieve = MockValidNotice();
            var idToRetrieve = noticeToRetrieve.Id;

            _repositoryMock.Setup(repo => repo.GetByIdAsync(idToRetrieve)).ReturnsAsync(noticeToRetrieve);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadNoticeOutput>(It.IsAny<Domain.Entities.Notice>())).Returns(new DetailedReadNoticeOutput());

            // Act
            var result = await useCase.ExecuteAsync(idToRetrieve);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(idToRetrieve), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NoticeNotFound_ReturnsNull()
        {
            // Arrange
            var useCase = CreateUseCase();
            var idToRetrieve = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(idToRetrieve)).ReturnsAsync((Domain.Entities.Notice)null);

            // Act
            var result = await useCase.ExecuteAsync(idToRetrieve);

            // Assert
            Assert.Null(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(idToRetrieve), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
