using System.Security.Claims;
using Application.Interfaces.UseCases.User;
using Application.UseCases.User;
using Application.Validation;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.User
{
    public class MakeCoordinatorTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new Mock<ITokenAuthenticationService>();

        private IMakeCoordinator CreateUseCase() => new MakeCoordinator(_userRepositoryMock.Object, _tokenAuthenticationServiceMock.Object);
        private static Domain.Entities.User MockValidUser() => new(id: Guid.NewGuid(), name: "Test", role: "ADMIN");
        private static Dictionary<Guid, Domain.Entities.User> MockUserClaims(Domain.Entities.User user) => new() { { (Guid)user.Id, user } };

        [Fact]
        public async Task ExecuteAsync_ValidUserId_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            adminUser.IsCoordinator = true;
            var userToMakeCoordinator = MockValidUser();
            userToMakeCoordinator.IsCoordinator = false;
            var userClaims = MockUserClaims(adminUser);
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUser.Id)).ReturnsAsync(adminUser);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userToMakeCoordinator.Id)).ReturnsAsync(userToMakeCoordinator);

            // Act
            var result = await useCase.ExecuteAsync(userToMakeCoordinator.Id);

            // Assert
            Assert.Equal($"Usuário {adminUser.Id} tornou coordenador o usuário {userToMakeCoordinator.Id}", result);
            Assert.False(adminUser.IsCoordinator);
            Assert.True(userToMakeCoordinator.IsCoordinator);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(adminUser), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(userToMakeCoordinator), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NullUserId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(null));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UnauthenticatedUser_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Dictionary<Guid, Domain.Entities.User> userClaims = null;
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(Guid.NewGuid()));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NonAdminUser_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            adminUser.Role = ERole.PROFESSOR;
            var userClaims = MockUserClaims(adminUser);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(Guid.NewGuid()));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_AdminUserDoesNotExist_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            adminUser.IsCoordinator = true;
            var adminUserId = adminUser.Id;
            var userToMakeCoordinator = MockValidUser();
            userToMakeCoordinator.IsCoordinator = false;
            var userClaims = MockUserClaims(adminUser);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUserId)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userToMakeCoordinator.Id));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(adminUserId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserToMakeCoordinatorDoesNotExist_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            adminUser.IsCoordinator = true;
            var userToMakeCoordinatorId = Guid.NewGuid();
            var userClaims = MockUserClaims(adminUser);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUser.Id)).ReturnsAsync(adminUser);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userToMakeCoordinatorId)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userToMakeCoordinatorId));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(adminUser.Id), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userToMakeCoordinatorId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_AdminUserIsNotCoordinator_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            adminUser.IsCoordinator = false;

            var userToMakeCoordinator = MockValidUser();
            userToMakeCoordinator.IsCoordinator = false;

            var userClaims = MockUserClaims(adminUser);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUser.Id)).ReturnsAsync(adminUser);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userToMakeCoordinator.Id)).ReturnsAsync(userToMakeCoordinator);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userToMakeCoordinator.Id));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(adminUser.Id), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userToMakeCoordinator.Id), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }
    }
}
