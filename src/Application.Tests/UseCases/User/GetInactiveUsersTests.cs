using AutoMapper;
using Application.UseCases.User;
using Application.Ports.User;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.User;

namespace Application.Tests.UseCases.User
{
    public class GetInactiveUsersTests
    {
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private IGetInactiveUsers CreateUseCase() => new GetInactiveUsers(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.User MockValidUser() => new(id: Guid.NewGuid(), name: "Test", role: "ADMIN");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsListOfUserReadOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = 0;
            int take = 10;
            var users = new List<Domain.Entities.User> { MockValidUser(), MockValidUser() };
            _repositoryMock.Setup(repo => repo.GetInactiveUsersAsync(skip, take)).ReturnsAsync(users);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserReadOutput>>(users)).Returns(users.Select(u => new UserReadOutput()).ToList());

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<UserReadOutput>>(result);
            Assert.Equal(users.Count, result.Count());
            _repositoryMock.Verify(repo => repo.GetInactiveUsersAsync(skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<UserReadOutput>>(users), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_InvalidSkipAndTake_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = -1;
            int take = 0;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(skip, take));
            _repositoryMock.Verify(repo => repo.GetInactiveUsersAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<UserReadOutput>>(It.IsAny<IEnumerable<Domain.Entities.User>>()), Times.Never);
        }
    }
}
