using Application.Interfaces.UseCases.Auth;
using Application.Ports.Auth;
using Application.UseCases.Auth;
using Application.Validation;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases.Auth
{
    public class LoginTests
    {
        private readonly Mock<ITokenAuthenticationService> _tokenServiceMock = new Mock<ITokenAuthenticationService>();
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IProfessorRepository> _professorRepositoryMock = new Mock<IProfessorRepository>();
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new Mock<IStudentRepository>();
        private readonly Mock<IHashService> _hashServiceMock = new Mock<IHashService>();

        private ILogin CreateUseCase() => new Login(_tokenServiceMock.Object, _userRepositoryMock.Object, _professorRepositoryMock.Object, _studentRepositoryMock.Object, _hashServiceMock.Object);
        private static Domain.Entities.User MockValidUser() => new("John Doe", "john.doe@example.com", "strongpassword", "92114660087", ERole.ADMIN);
        private static Domain.Entities.User MockValidUserWithId() => new(Guid.NewGuid(), "John Doe", "ADMIN");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsUserLoginOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserLoginInput
            {
                Email = "test@example.com",
                Password = "password"
            };
            var user = MockValidUserWithId();
            user.Password = "hashed_password";
            user.Email = input.Email;
            user.ConfirmUserEmail(user.ValidationCode);
            user.Role = ERole.PROFESSOR;
            var professor = new Domain.Entities.Professor(Guid.NewGuid(), "1234567", 1234567)
            {
                UserId = user.Id
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync(user);
            _professorRepositoryMock.Setup(repo => repo.GetByUserIdAsync(user.Id)).ReturnsAsync(professor);
            _hashServiceMock.Setup(hashService => hashService.VerifyPassword(input.Password, user.Password)).Returns(true);
            _tokenServiceMock.Setup(tokenService => tokenService.GenerateToken(user.Id, professor.Id, user.Name, user.Role.ToString())).Returns("token");

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("token", result.Token);
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(input.Email), Times.Once);
            _hashServiceMock.Verify(hashService => hashService.VerifyPassword(input.Password, user.Password), Times.Once);
            _tokenServiceMock.Verify(tokenService => tokenService.GenerateToken(user.Id, professor.Id, user.Name, user.Role.ToString()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_EmailIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserLoginInput
            {
                Email = null,
                Password = "password"
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _hashServiceMock.Verify(hashService => hashService.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _tokenServiceMock.Verify(tokenService => tokenService.GenerateToken(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_PasswordIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserLoginInput
            {
                Email = "test@example.com",
                Password = null
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _hashServiceMock.Verify(hashService => hashService.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _tokenServiceMock.Verify(tokenService => tokenService.GenerateToken(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserLoginInput
            {
                Email = "test@example.com",
                Password = "password"
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(input.Email), Times.Once);
            _hashServiceMock.Verify(hashService => hashService.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _tokenServiceMock.Verify(tokenService => tokenService.GenerateToken(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotConfirmed_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var input = new UserLoginInput
            {
                Email = user.Email,
                Password = user.Password
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(input.Email), Times.Once);
            _hashServiceMock.Verify(hashService => hashService.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _tokenServiceMock.Verify(tokenService => tokenService.GenerateToken(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_InvalidPassword_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var input = new UserLoginInput
            {
                Email = user.Email,
                Password = user.Password
            };
            user.ConfirmUserEmail(user.ValidationCode);

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync(user);
            _hashServiceMock.Setup(hashService => hashService.VerifyPassword(input.Password, user.Password)).Returns(false);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(input.Email), Times.Once);
            _hashServiceMock.Verify(hashService => hashService.VerifyPassword(input.Password, user.Password), Times.Once);
            _tokenServiceMock.Verify(tokenService => tokenService.GenerateToken(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
