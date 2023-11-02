using Application.Interfaces.UseCases.Auth;
using Application.UseCases.Auth;
using Application.Validation;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Auth
{
    public class ForgotPasswordTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();

        private IForgotPassword CreateUseCase() => new ForgotPassword(_userRepositoryMock.Object, _emailServiceMock.Object);

        private static Domain.Entities.User MockValidUser() => new("John Doe", "john.doe@example.com", "strongpassword", "92114660087", ERole.ADMIN);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var user = MockValidUser();
            var email = user.Email;

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var result = await useCase.ExecuteAsync(email);

            // Assert
            Assert.Equal("Token de recuperação gerado e enviado por e-mail com sucesso.", result);
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
            Assert.NotNull(user.ResetPasswordToken); // Verifica se o token foi gerado
            _emailServiceMock.Verify(service => service.SendResetPasswordEmailAsync(user.Email, user.Name, user.ResetPasswordToken), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_EmailIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            string email = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendResetPasswordEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var email = "test@example.com";

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendResetPasswordEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
