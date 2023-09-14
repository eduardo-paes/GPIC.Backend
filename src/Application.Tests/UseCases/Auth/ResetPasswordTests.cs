using Application.Interfaces.UseCases.Auth;
using Application.Ports.Auth;
using Application.UseCases.Auth;
using Application.Validation;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Auth
{
    public class ResetPasswordTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IHashService> _hashServiceMock = new Mock<IHashService>();

        private IResetPassword CreateUseCase() => new ResetPassword(_userRepositoryMock.Object, _hashServiceMock.Object);
        private static User MockValidUser() => new("John Doe", "john.doe@example.com", "strongpassword", "92114660087", ERole.ADMIN);
        private static User MockValidUserWithId() => new(Guid.NewGuid(), "John Doe", "ADMIN");


        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUserWithId();
            user.GenerateResetPasswordToken();
            user.Password = "hashed_password";
            var input = new UserResetPasswordInput
            {
                Id = user.Id,
                Password = "new_password",
                Token = user.ResetPasswordToken
            };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(input.Id.Value)).ReturnsAsync(user);
            _hashServiceMock.Setup(hashService => hashService.HashPassword(input.Password)).Returns("hashed_password");

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Senha atualizada com sucesso.", result);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(input.Id.Value), Times.Once);
            _hashServiceMock.Verify(hashService => hashService.HashPassword("new_password"), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserResetPasswordInput
            {
                Id = null,
                Password = "new_password",
                Token = "reset_token"
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_PasswordIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserResetPasswordInput
            {
                Id = Guid.NewGuid(),
                Password = null,
                Token = "reset_token"
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_TokenIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserResetPasswordInput
            {
                Id = Guid.NewGuid(),
                Password = "new_password",
                Token = null
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserResetPasswordInput
            {
                Id = Guid.NewGuid(),
                Password = "new_password",
                Token = "reset_token"
            };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(input.Id.Value)).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(input.Id.Value), Times.Once);
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_InvalidToken_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUserWithId();
            user.GenerateResetPasswordToken();
            var input = new UserResetPasswordInput
            {
                Id = user.Id,
                Password = "new_password",
                Token = "different_token"
            };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(input.Id.Value)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(input.Id.Value), Times.Once);
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
