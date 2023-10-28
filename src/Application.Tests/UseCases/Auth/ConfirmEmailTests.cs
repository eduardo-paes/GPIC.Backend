using Application.Interfaces.UseCases.Auth;
using Application.UseCases.Auth;
using Application.Validation;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Auth
{
    public class ConfirmEmailTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();

        private IConfirmEmail CreateUseCase() => new ConfirmEmail(_userRepositoryMock.Object);

        private static Domain.Entities.User MockValidUser() => new("John Doe", "john.doe@example.com", "strongpassword", "92114660087", ERole.ADMIN);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var email = user.Email;
            var token = user.ValidationCode;

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var result = await useCase.ExecuteAsync(email, token);

            // Assert
            Assert.Equal("Usuário confirmado com sucesso.", result);
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
            Assert.True(user.IsConfirmed);
        }

        [Fact]
        public void ExecuteAsync_EmailIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            string email = null;
            var token = "validtoken";

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email, token));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_TokenIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var email = "test@example.com";
            string token = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email, token));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var email = "test@example.com";
            var token = "validtoken";

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email, token));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_InvalidToken_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var email = user.Email;
            var token = "invalidtoken";

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email, token));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserAlreadyConfirmed_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var token = user.ValidationCode;
            var email = user.Email;
            user.ConfirmUserEmail(token);

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email, token));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }
    }
}
