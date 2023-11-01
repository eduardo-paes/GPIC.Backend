using AutoMapper;
using Application.UseCases.User;
using Application.Ports.User;
using Application.Validation;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.User;

namespace Application.Tests.UseCases.User
{
    public class DeactivateUserTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeactivateUser CreateUseCase() => new DeactivateUser(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.User MockValidUser() => new(id: Guid.NewGuid(), name: "Test", role: "ADMIN");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsUserReadOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var id = user.Id;

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(user);
            _repositoryMock.Setup(repo => repo.UpdateAsync(user)).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<UserReadOutput>(user)).Returns(new UserReadOutput());

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(user), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_InvalidId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid id = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(It.IsAny<Domain.Entities.User>()), Times.Never);
        }
    }
}
