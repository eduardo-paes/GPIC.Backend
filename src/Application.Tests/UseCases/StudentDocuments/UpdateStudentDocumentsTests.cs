using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Ports.StudentDocuments;
using Application.Validation;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using Application.UseCases.StudentDocuments;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Application.Tests.UseCases.StudentDocuments
{
    public class UpdateStudentDocumentsTests
    {
        private readonly Mock<IStudentDocumentsRepository> _studentDocumentRepositoryMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IStorageFileService> _storageFileServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateStudentDocuments CreateUseCase() =>
            new UpdateStudentDocuments(
                _studentDocumentRepositoryMock.Object,
                _projectRepositoryMock.Object,
                _storageFileServiceMock.Object,
                _mapperMock.Object);
        private static Domain.Entities.StudentDocuments MockValidStudentDocuments() => new(Guid.NewGuid(), "123456", "7890");
        private static Project MockValidProject() => new(
                    "Project Title",
                    "Keyword 1",
                    "Keyword 2",
                    "Keyword 3",
                    true,
                    "Objective",
                    "Methodology",
                    "Expected Results",
                    "Schedule",
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    EProjectStatus.Accepted,
                    "Status Description",
                    "Appeal Observation",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    "Cancellation Reason");

        [Fact]
        public async Task ExecuteAsync_ValidIdAndInput_ReturnsDetailedReadStudentDocumentsOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateStudentDocumentsInput
            {
                AgencyNumber = "1234",
                AccountNumber = "5678",
            };
            var project = MockValidProject();
            var studentDocuments = MockValidStudentDocuments();
            studentDocuments.Project = project;

            _studentDocumentRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(studentDocuments);
            _studentDocumentRepositoryMock.Setup(r => r.UpdateAsync(studentDocuments)).ReturnsAsync(studentDocuments);
            _storageFileServiceMock.Setup(s => s.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>())).ReturnsAsync("file_url");
            _projectRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project);
            _mapperMock.Setup(m => m.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>())).Returns(new DetailedReadStudentDocumentsOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            _studentDocumentRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.AtMostOnce);
            _studentDocumentRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.AtMost(7));
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;
            var input = new UpdateStudentDocumentsInput();

            // Act & Assert
            var exception = Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _studentDocumentRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Never);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _studentDocumentRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Never);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_StudentDocumentsNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateStudentDocumentsInput();

            _studentDocumentRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.StudentDocuments)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _studentDocumentRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _studentDocumentRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Never);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.Never);
        }
    }
}
