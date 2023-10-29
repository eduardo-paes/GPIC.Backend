using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.StudentDocuments;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.StudentDocuments;

namespace Application.Tests.UseCases.StudentDocuments
{
    public class GetStudentDocumentsByProjectIdTests
    {
        private readonly Mock<IStudentDocumentsRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetStudentDocumentsByProjectId CreateUseCase() => new GetStudentDocumentsByProjectId(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.StudentDocuments MockValidStudentDocuments() => new(Guid.NewGuid(), "123456", "7890");

        [Fact]
        public async Task ExecuteAsync_ValidProjectId_ReturnsResumedReadStudentDocumentsOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var studentDocuments = MockValidStudentDocuments();

            _repositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync(studentDocuments);
            _mapperMock.Setup(mapper => mapper.Map<ResumedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>())).Returns(new ResumedReadStudentDocumentsOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_ProjectIdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? projectId = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_RepositoryReturnsNull_ReturnsNullOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync((Domain.Entities.StudentDocuments)null);

            // Act
            var result = useCase.ExecuteAsync(projectId).Result;

            // Assert
            Assert.Null(result);
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Once);
        }
    }
}
