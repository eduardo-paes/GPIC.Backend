using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.Notice;

namespace Application.Tests.UseCases.Notice
{
    public class DeleteNoticeTests
    {
        private readonly Mock<INoticeRepository> _repositoryMock = new();
        private readonly Mock<IStorageFileService> _storageFileServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteNotice CreateUseCase() => new DeleteNotice(_repositoryMock.Object, _storageFileServiceMock.Object, _mapperMock.Object);
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
        public async Task ExecuteAsync_ValidId_DeletesNoticeAndFile()
        {
            // Arrange
            var useCase = CreateUseCase();
            var noticeToDelete = MockValidNotice();
            var idToDelete = noticeToDelete.Id;
            noticeToDelete.DocUrl = "file-url";

            _repositoryMock.Setup(repo => repo.DeleteAsync(idToDelete)).ReturnsAsync(noticeToDelete);
            _storageFileServiceMock.Setup(service => service.DeleteFileAsync(noticeToDelete.DocUrl));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadNoticeOutput>(It.IsAny<Domain.Entities.Notice>())).Returns(new DetailedReadNoticeOutput());

            // Act
            var result = await useCase.ExecuteAsync(idToDelete);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.DeleteAsync(idToDelete), Times.Once);
            _storageFileServiceMock.Verify(service => service.DeleteFileAsync(noticeToDelete.DocUrl), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NoticeNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var idToDelete = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.DeleteAsync(idToDelete)).ReturnsAsync((Domain.Entities.Notice)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(idToDelete));
            _repositoryMock.Verify(repo => repo.DeleteAsync(idToDelete), Times.Once);
            _storageFileServiceMock.Verify(service => service.DeleteFileAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_FileDeletionFails_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var noticeToDelete = MockValidNotice();
            var idToDelete = noticeToDelete.Id;
            noticeToDelete.DocUrl = "file-url";

            _repositoryMock.Setup(repo => repo.DeleteAsync(idToDelete)).ReturnsAsync(noticeToDelete);
            _storageFileServiceMock.Setup(service => service.DeleteFileAsync(noticeToDelete.DocUrl));

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(idToDelete));
            _repositoryMock.Verify(repo => repo.DeleteAsync(idToDelete), Times.Once);
            _storageFileServiceMock.Verify(service => service.DeleteFileAsync(noticeToDelete.DocUrl), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _storageFileServiceMock.Verify(service => service.DeleteFileAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
