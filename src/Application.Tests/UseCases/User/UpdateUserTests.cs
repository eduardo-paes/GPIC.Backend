using Application.Interfaces.UseCases.User;
using Application.Ports.User;
using Application.UseCases.User;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.User
{
    public class UpdateUserTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateUser CreateUseCase() =>
            new UpdateUser(_userRepositoryMock.Object, _tokenAuthenticationServiceMock.Object, _mapperMock.Object);
        private static Domain.Entities.User MockValidUser() => new(id: Guid.NewGuid(), name: "Test", role: "ADMIN");
        private static Dictionary<Guid, Domain.Entities.User> MockUserClaims(Domain.Entities.User user) => new() { { (Guid)user.Id, user } };

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsUpdatedUser()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserUpdateInput
            {
                Name = "New Name",
                CPF = "83278087020"
            };
            var existingUser = MockValidUser();
            var userClaims = MockUserClaims(existingUser);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(existingUser);
            _mapperMock.Setup(mapper => mapper.Map<UserReadOutput>(It.IsAny<Domain.Entities.User>())).Returns(new UserReadOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.Name, existingUser.Name);
            Assert.Equal(input.CPF, existingUser.CPF);
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(existingUser), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(existingUser), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NullUserId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserUpdateInput
            {
                Name = "New Name",
                CPF = "12345678901"
            };
            Dictionary<Guid, Domain.Entities.User> userClaims = null;
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserDoesNotExist_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UserUpdateInput
            {
                Name = "New Name",
                CPF = "12345678901"
            };
            var user = MockValidUser();
            var userId = user.Id;
            var userClaims = MockUserClaims(user);
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<UserReadOutput>(It.IsAny<Domain.Entities.User>()), Times.Never);
        }
    }
}
