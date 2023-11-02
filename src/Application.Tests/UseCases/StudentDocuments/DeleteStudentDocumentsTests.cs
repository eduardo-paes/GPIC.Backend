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
    public class DeleteStudentDocumentsTests
    {
        private readonly Mock<IStudentDocumentsRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteStudentDocuments CreateUseCase() => new DeleteStudentDocuments(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.StudentDocuments MockValidStudentDocuments() => new(Guid.NewGuid(), "123456", "7890");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadStudentDocumentsOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var deletedStudentDocuments = MockValidStudentDocuments();

            _repositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(deletedStudentDocuments);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>())).Returns(new DetailedReadStudentDocumentsOutput());

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.DeleteAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_RepositoryReturnsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync((Domain.Entities.StudentDocuments)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.DeleteAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentDocumentsOutput>(It.IsAny<Domain.Entities.StudentDocuments>()), Times.Once);
        }
    }
}
