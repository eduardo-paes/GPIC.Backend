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
    public class GetUserByIdTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private IGetUserById CreateUseCase() => new GetUserById(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.User MockValidUser() => new(id: Guid.NewGuid(), name: "Test", role: "ADMIN");

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsUserReadOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var userId = user.Id;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<UserReadOutput>(user)).Returns(new UserReadOutput());

            // Act
            var result = await useCase.ExecuteAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserReadOutput>(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(user), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? userId = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userId));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NonexistentId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var userId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userId));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(It.IsAny<Domain.Entities.User>()), Times.Once);
        }
    }
}
