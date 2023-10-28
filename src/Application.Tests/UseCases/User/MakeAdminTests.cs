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
    public class MakeAdminTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IProfessorRepository> _professorRepositoryMock = new Mock<IProfessorRepository>();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new Mock<ITokenAuthenticationService>();

        private IMakeAdmin CreateUseCase() =>
            new MakeAdmin(_userRepositoryMock.Object, _professorRepositoryMock.Object, _tokenAuthenticationServiceMock.Object);
        private static Domain.Entities.Professor MockValidProfessor() => new("1234567", 12345);
        private static Domain.Entities.User MockValidUser() => new(id: Guid.NewGuid(), name: "Test", role: "ADMIN");
        private static Dictionary<Guid, Domain.Entities.User> MockUserClaims(Domain.Entities.User user) => new() { { (Guid)user.Id, user } };

        [Fact]
        public async Task ExecuteAsync_ValidUserId_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            var userToMakeAdmin = MockValidUser();
            userToMakeAdmin.Role = ERole.PROFESSOR;

            var userClaims = MockUserClaims(adminUser);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUser.Id)).ReturnsAsync(adminUser);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userToMakeAdmin.Id)).ReturnsAsync(userToMakeAdmin);
            _professorRepositoryMock.Setup(repo => repo.GetByUserIdAsync(userToMakeAdmin.Id)).ReturnsAsync(MockValidProfessor());

            // Act
            var result = await useCase.ExecuteAsync(userToMakeAdmin.Id);

            // Assert
            Assert.Equal($"Usuário {adminUser.Id} tornou administrador o usuário {userToMakeAdmin.Id}", result);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(userToMakeAdmin), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NullUserId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(null));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _professorRepositoryMock.Verify(repo => repo.GetByUserIdAsync(It.IsAny<Guid?>()), Times.Never);
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
            _professorRepositoryMock.Verify(repo => repo.GetByUserIdAsync(It.IsAny<Guid?>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NonAdminUser_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            user.Role = ERole.PROFESSOR;

            var userClaims = MockUserClaims(user);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(Guid.NewGuid()));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _professorRepositoryMock.Verify(repo => repo.GetByUserIdAsync(It.IsAny<Guid?>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_AdminUserDoesNotExist_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var userToMakeAdmin = MockValidUser();
            userToMakeAdmin.Role = ERole.PROFESSOR;

            var adminUser = MockValidUser();
            var adminUserId = adminUser.Id;
            var userClaims = MockUserClaims(adminUser);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUserId)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userToMakeAdmin.Id));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(adminUserId), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.GetByUserIdAsync(It.IsAny<Guid?>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }


        [Fact]
        public void ExecuteAsync_UserToMakeAdminDoesNotExist_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            var userToMakeAdminId = Guid.NewGuid();
            var userClaims = MockUserClaims(adminUser);
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUser.Id)).ReturnsAsync(adminUser);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userToMakeAdminId)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userToMakeAdminId));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(adminUser.Id), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userToMakeAdminId), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.GetByUserIdAsync(It.IsAny<Guid?>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserToMakeAdminIsNotProfessor_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var adminUser = MockValidUser();
            var userToMakeAdmin = MockValidUser();
            userToMakeAdmin.Role = ERole.PROFESSOR;
            var userClaims = MockUserClaims(adminUser);
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(adminUser.Id)).ReturnsAsync(adminUser);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userToMakeAdmin.Id)).ReturnsAsync(userToMakeAdmin);
            _professorRepositoryMock.Setup(repo => repo.GetByUserIdAsync(userToMakeAdmin.Id)).ReturnsAsync((Domain.Entities.Professor)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(userToMakeAdmin.Id));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(adminUser.Id), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userToMakeAdmin.Id), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.GetByUserIdAsync(userToMakeAdmin.Id), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }
    }
}
