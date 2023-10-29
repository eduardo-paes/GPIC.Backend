using Application.Interfaces.UseCases.StudentDocuments;
using Application.Ports.StudentDocuments;
using Application.UseCases.StudentDocuments;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.StudentDocuments
{
    public class CreateStudentDocumentsTests
    {
        private readonly Mock<IStudentDocumentsRepository> _studentDocumentRepositoryMock = new Mock<IStudentDocumentsRepository>();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new Mock<IProjectRepository>();
        private readonly Mock<IStorageFileService> _storageFileServiceMock = new Mock<IStorageFileService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private ICreateStudentDocuments CreateUseCase() => new CreateStudentDocuments(_studentDocumentRepositoryMock.Object, _projectRepositoryMock.Object,
                _storageFileServiceMock.Object, _mapperMock.Object);
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
            EProjectStatus.Opened,
            "Status Description",
            "Appeal Observation",
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Cancellation Reason");
        private static Domain.Entities.StudentDocuments MockValidStudentDocuments()
        {
            return new Domain.Entities.StudentDocuments(
                            Guid.NewGuid(),
                            "123456",
                            "7890"
                        );
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadStudentDocumentsOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var project = MockValidProject();
            project.Status = EProjectStatus.Accepted;
            var input = new CreateStudentDocumentsInput
            {
                ProjectId = Guid.NewGuid(),
                AgencyNumber = "123456",
                AccountNumber = "7890",
                IdentityDocument = CreateIFormFile(),
                CPF = CreateIFormFile(),
                Photo3x4 = CreateIFormFile(),
                SchoolHistory = CreateIFormFile(),
                ScholarCommitmentAgreement = CreateIFormFile(),
                AccountOpeningProof = CreateIFormFile()
            };

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _studentDocumentRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync((Domain.Entities.StudentDocuments)null);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(It.IsAny<IFormFile>(), null)).ReturnsAsync("file_url");
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()))
                .Returns(new DetailedReadStudentDocumentsOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _studentDocumentRepositoryMock.Verify(repo => repo.GetByProjectIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), null), Times.Exactly(6));
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_DocumentsExist_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateStudentDocumentsInput
            {
                ProjectId = Guid.NewGuid(),
                IdentityDocument = CreateIFormFile(),
                CPF = CreateIFormFile(),
                Photo3x4 = CreateIFormFile(),
                SchoolHistory = CreateIFormFile(),
                ScholarCommitmentAgreement = CreateIFormFile(),
                AccountOpeningProof = CreateIFormFile()
            };

            var existingDocuments = MockValidStudentDocuments();
            existingDocuments.ProjectId = input.ProjectId;

            _studentDocumentRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(existingDocuments);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _studentDocumentRepositoryMock.Verify(repo => repo.GetByProjectIdAsync(input.ProjectId), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Never);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), ""), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Never);
        }


        [Fact]
        public void ExecuteAsync_ProjectNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateStudentDocumentsInput
            {
                ProjectId = Guid.NewGuid(),
                IdentityDocument = CreateIFormFile(),
                CPF = CreateIFormFile(),
                Photo3x4 = CreateIFormFile(),
                SchoolHistory = CreateIFormFile(),
                ScholarCommitmentAgreement = CreateIFormFile(),
                AccountOpeningProof = CreateIFormFile()
            };

            _studentDocumentRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync((Domain.Entities.StudentDocuments)null);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync((Project)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _studentDocumentRepositoryMock.Verify(repo => repo.GetByProjectIdAsync(input.ProjectId), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), ""), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_ProjectNotAccepted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var project = MockValidProject();
            project.Status = EProjectStatus.Pending; // Not Accepted
            var input = new CreateStudentDocumentsInput
            {
                ProjectId = Guid.NewGuid(),
                IdentityDocument = CreateIFormFile(),
                CPF = CreateIFormFile(),
                Photo3x4 = CreateIFormFile(),
                SchoolHistory = CreateIFormFile(),
                ScholarCommitmentAgreement = CreateIFormFile(),
                AccountOpeningProof = CreateIFormFile()
            };

            _studentDocumentRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync((Domain.Entities.StudentDocuments)null);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _studentDocumentRepositoryMock.Verify(repo => repo.GetByProjectIdAsync(input.ProjectId), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), ""), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Never);
        }

        private IFormFile CreateIFormFile()
        {
            // Create a mock IFormFile, you can adjust the implementation as needed.
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(file => file.FileName).Returns("file.txt");
            fileMock.Setup(file => file.Length).Returns(1024);
            // Add other setup as needed.
            return fileMock.Object;
        }
    }
}
