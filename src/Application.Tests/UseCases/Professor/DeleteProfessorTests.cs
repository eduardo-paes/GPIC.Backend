using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Professor;
using Application.Ports.Professor;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.Professor;
using Domain.Entities.Enums;

namespace Application.Tests.UseCases.Professor
{
    public class DeleteProfessorTests
    {
        private readonly Mock<IProfessorRepository> _professorRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteProfessor CreateUseCase() => new DeleteProfessor(_professorRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
        private Domain.Entities.User CreateUser() => new(Guid.NewGuid(), "John Doe", "john.doe@email.com", "123456789", "58411338029", ERole.PROFESSOR);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProfessorOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var professorId = Guid.NewGuid();
            var user = CreateUser();
            var professor = new Domain.Entities.Professor(professorId, "SIAPE12", 1234567)
            {
                UserId = user.Id
            };

            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync(professor);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);

            _professorRepositoryMock.Setup(repo => repo.DeleteAsync(professorId)).ReturnsAsync(professor);
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(user.Id)).ReturnsAsync(user);

            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProfessorOutput>(professor)).Returns(new DetailedReadProfessorOutput());

            // Act
            var result = await useCase.ExecuteAsync(professorId);

            // Assert
            Assert.NotNull(result);
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.DeleteAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(user.Id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(professor), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? professorId = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(professorId));
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _professorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_ProfessorNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var professorId = Guid.NewGuid();

            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync((Domain.Entities.Professor)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(professorId));
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _professorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var professorId = Guid.NewGuid();
            var professor = new Domain.Entities.Professor(professorId, "SIAPE12", 1234567)
            {
                UserId = Guid.NewGuid()
            };

            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync(professor);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(professor.UserId)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(professorId));
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(professor.UserId), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_ProfessorNotDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var professorId = Guid.NewGuid();
            var user = CreateUser();
            var professor = new Domain.Entities.Professor(professorId, "SIAPE12", 1234567)
            {
                UserId = user.Id
            };

            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync(professor);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);

            _professorRepositoryMock.Setup(repo => repo.DeleteAsync(professorId)).ReturnsAsync((Domain.Entities.Professor)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(professorId));
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.DeleteAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(user.Id), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var professorId = Guid.NewGuid();
            var user = CreateUser();
            var professor = new Domain.Entities.Professor(professorId, "SIAPE12", 1234567)
            {
                UserId = user.Id
            };

            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync(professor);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);

            _professorRepositoryMock.Setup(repo => repo.DeleteAsync(professorId)).ReturnsAsync(professor);
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(user.Id)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(professorId));
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.DeleteAsync(professorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(user.Id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }
    }
}
