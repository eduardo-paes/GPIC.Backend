using Application.Interfaces.UseCases.Professor;
using Application.Ports.Professor;
using Application.UseCases.Professor;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Professor
{
    public class CreateProfessorTests
    {
        private readonly Mock<IProfessorRepository> _professorRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<IHashService> _hashServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateProfessor CreateUseCase() => new CreateProfessor(
            _professorRepositoryMock.Object,
            _userRepositoryMock.Object,
            _emailServiceMock.Object,
            _hashServiceMock.Object,
            _mapperMock.Object);
        private static Domain.Entities.User MockValidUser() => new("John Doe", "john.doe@example.com", "strongpassword", "92114660087", ERole.ADMIN);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProfessorOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var password = "Password123";
            var input = new CreateProfessorInput
            {
                SIAPEEnrollment = "1234567",
                IdentifyLattes = 1234567,
                Password = password,
                Name = "Professor Name",
                Email = "professor@example.com",
                CPF = "58411338029"
            };
            var hashedPassword = "HashedPassword123";
            var user = new Domain.Entities.User(Guid.NewGuid(), input.Name, input.Email, hashedPassword, input.CPF, ERole.PROFESSOR);
            var professor = new Domain.Entities.Professor(input.SIAPEEnrollment, input.IdentifyLattes)
            {
                UserId = user.Id
            };

            _hashServiceMock.Setup(hashService => hashService.HashPassword(input.Password)).Returns(hashedPassword);
            _userRepositoryMock.Setup(userRepository => userRepository.GetUserByEmailAsync(input.Email)).ReturnsAsync((Domain.Entities.User)null);
            _userRepositoryMock.Setup(userRepository => userRepository.GetUserByCPFAsync(input.CPF)).ReturnsAsync((Domain.Entities.User)null);
            _userRepositoryMock.Setup(userRepository => userRepository.CreateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(user);
            _professorRepositoryMock.Setup(professorRepository => professorRepository.CreateAsync(It.IsAny<Domain.Entities.Professor>())).ReturnsAsync(professor);
            _emailServiceMock.Setup(emailService => emailService.SendConfirmationEmailAsync(user.Email, user.Name, user.ValidationCode)).Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>())).Returns(new DetailedReadProfessorOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _hashServiceMock.Verify(hashService => hashService.HashPassword(password), Times.Once);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByEmailAsync(input.Email), Times.Once);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByCPFAsync(input.CPF), Times.Once);
            _userRepositoryMock.Verify(userRepository => userRepository.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Once);
            _professorRepositoryMock.Verify(professorRepository => professorRepository.CreateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Once);
            _emailServiceMock.Verify(emailService => emailService.SendConfirmationEmailAsync(user.Email, user.Name, user.ValidationCode), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_PasswordIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateProfessorInput
            {
                Password = null
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByEmailAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByCPFAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(userRepository => userRepository.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _professorRepositoryMock.Verify(professorRepository => professorRepository.CreateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Never);
            _emailServiceMock.Verify(emailService => emailService.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserWithEmailExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateProfessorInput
            {
                SIAPEEnrollment = "1234567",
                IdentifyLattes = 1234567,
                Email = "existing@example.com",
                CPF = "58411338029",
                Name = "Professor Name",
                Password = "Password123"
            };

            _userRepositoryMock.Setup(userRepository => userRepository.GetUserByEmailAsync(input.Email)).ReturnsAsync(MockValidUser());

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByEmailAsync(input.Email), Times.Once);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByCPFAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(userRepository => userRepository.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _professorRepositoryMock.Verify(professorRepository => professorRepository.CreateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Never);
            _emailServiceMock.Verify(emailService => emailService.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_UserWithCPFExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateProfessorInput
            {
                SIAPEEnrollment = "1234567",
                IdentifyLattes = 1234567,
                Email = "existing@example.com",
                CPF = "58411338029",
                Name = "Professor Name",
                Password = "Password123"
            };

            _userRepositoryMock.Setup(userRepository => userRepository.GetUserByCPFAsync(input.CPF)).ReturnsAsync(MockValidUser());

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _hashServiceMock.Verify(hashService => hashService.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(userRepository => userRepository.GetUserByCPFAsync(input.CPF), Times.Once);
            _userRepositoryMock.Verify(userRepository => userRepository.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _professorRepositoryMock.Verify(professorRepository => professorRepository.CreateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Never);
            _emailServiceMock.Verify(emailService => emailService.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }
    }
}
