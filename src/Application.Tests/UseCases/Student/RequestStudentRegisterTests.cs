using Application.Interfaces.UseCases.Student;
using Application.Tests.Mocks;
using Application.UseCases.Student;
using Application.Validation;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Student
{
    public class RequestStudentRegisterTests
    {
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();

        private IRequestStudentRegister CreateUseCase() => new RequestStudentRegister(
            _emailServiceMock.Object,
            _userRepositoryMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidEmail_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var email = "john.doe@aluno.cefet-rj.br";

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync((Domain.Entities.User)null);

            // Act
            var result = await useCase.ExecuteAsync(email);

            // Assert
            Assert.Equal("Solicitação de registro enviada com sucesso.", result);
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _emailServiceMock.Verify(service => service.SendRequestStudentRegisterEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_EmptyEmail_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            string email = null;

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email));

            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendRequestStudentRegisterEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidEmail_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var email = "invalid.email";

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email));

            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendRequestStudentRegisterEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_EmailAlreadyExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var email = "john.doe@aluno.cefet-rj.br";
            var user = UserMock.MockValidUser();

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email));

            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _emailServiceMock.Verify(service => service.SendRequestStudentRegisterEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData("jane.doe@domain.com")]
        [InlineData("12345678912@invalid-domain.com")]
        public async Task ExecuteAsync_InvalidStudentEmail_ThrowsUseCaseException(string email)
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(email));

            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendRequestStudentRegisterEmailAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
