using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.Tests.Mocks;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Student
{
    public class CreateStudentTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<ICampusRepository> _campusRepositoryMock = new();
        private readonly Mock<ICourseRepository> _courseRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<IHashService> _hashServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateStudent CreateUseCase() => new Application.UseCases.Student.CreateStudent(
            _studentRepositoryMock.Object,
            _userRepositoryMock.Object,
            _campusRepositoryMock.Object,
            _courseRepositoryMock.Object,
            _emailServiceMock.Object,
            _hashServiceMock.Object,
            _mapperMock.Object
        );
        public static CreateStudentInput GetValidInput()
        {
            return new CreateStudentInput
            {
                // Required Properties
                Name = "John Doe",
                CPF = "76500130065",
                Email = "john.doe@example.com",
                Password = "SecurePassword",
                RegistrationCode = "ABC123",
                BirthDate = new DateTime(1990, 1, 1),
                RG = 1234567,
                IssuingAgency = "Agency",
                DispatchDate = new DateTime(2000, 1, 1),
                Gender = 0, // Assuming 1 represents a gender value
                Race = 0, // Assuming 2 represents a race value
                HomeAddress = "123 Main St",
                City = "City",
                UF = "CA",
                CEP = 123456,
                CampusId = Guid.NewGuid(),
                CourseId = Guid.NewGuid(),
                StartYear = "2023",
                AssistanceTypeId = Guid.NewGuid(),

                // Optional Properties
                PhoneDDD = 123,
                Phone = 987654321,
                CellPhoneDDD = 456,
                CellPhone = 654321987
            };
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadStudentOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var user = UserMock.MockValidUser();
            var student = StudentMock.MockValidStudent();

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync((Domain.Entities.User)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByCPFAsync(input.CPF)).ReturnsAsync((Domain.Entities.User)null);
            _courseRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CourseId)).ReturnsAsync(CourseMock.MockValidCourse());
            _campusRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CampusId)).ReturnsAsync(CampusMock.MockValidCampus());
            _hashServiceMock.Setup(service => service.HashPassword(input.Password)).Returns("hashedPassword");
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(user);
            _studentRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>())).ReturnsAsync(student);
            _emailServiceMock.Setup(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByCPFAsync(It.IsAny<string>()), Times.Once);
            _courseRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _campusRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _hashServiceMock.Verify(service => service.HashPassword(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>()), Times.Once);
            _emailServiceMock.Verify(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_UserWithEmailExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var user = UserMock.MockValidUser();
            var student = StudentMock.MockValidStudent();

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.GetUserByCPFAsync(input.CPF)).ReturnsAsync((Domain.Entities.User)null);
            _courseRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CourseId)).ReturnsAsync(CourseMock.MockValidCourse());
            _campusRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CampusId)).ReturnsAsync(CampusMock.MockValidCampus());
            _hashServiceMock.Setup(service => service.HashPassword(input.Password)).Returns("hashedPassword");
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(user);
            _studentRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>())).ReturnsAsync(student);
            _emailServiceMock.Setup(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByCPFAsync(It.IsAny<string>()), Times.Never);
            _courseRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _campusRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _hashServiceMock.Verify(service => service.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _studentRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_UserWithCPFExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var user = UserMock.MockValidUser();
            var student = StudentMock.MockValidStudent();

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync((Domain.Entities.User)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByCPFAsync(input.CPF)).ReturnsAsync(user);
            _courseRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CourseId)).ReturnsAsync(CourseMock.MockValidCourse());
            _campusRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CampusId)).ReturnsAsync(CampusMock.MockValidCampus());
            _hashServiceMock.Setup(service => service.HashPassword(input.Password)).Returns("hashedPassword");
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(user);
            _studentRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>())).ReturnsAsync(student);
            _emailServiceMock.Setup(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByCPFAsync(It.IsAny<string>()), Times.Once);
            _courseRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _campusRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _hashServiceMock.Verify(service => service.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _studentRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_CourseNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var user = UserMock.MockValidUser();
            var student = StudentMock.MockValidStudent();

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync((Domain.Entities.User)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByCPFAsync(input.CPF)).ReturnsAsync((Domain.Entities.User)null);
            _courseRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CourseId)).ReturnsAsync((Domain.Entities.Course)null);
            _campusRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CampusId)).ReturnsAsync(CampusMock.MockValidCampus());
            _hashServiceMock.Setup(service => service.HashPassword(input.Password)).Returns("hashedPassword");
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(user);
            _studentRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>())).ReturnsAsync(student);
            _emailServiceMock.Setup(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByCPFAsync(It.IsAny<string>()), Times.Once);
            _courseRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _campusRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _hashServiceMock.Verify(service => service.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _studentRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_CampusNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var user = UserMock.MockValidUser();
            var student = StudentMock.MockValidStudent();

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync((Domain.Entities.User)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByCPFAsync(input.CPF)).ReturnsAsync((Domain.Entities.User)null);
            _courseRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CourseId)).ReturnsAsync(CourseMock.MockValidCourse());
            _campusRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CampusId)).ReturnsAsync((Domain.Entities.Campus)null);
            _hashServiceMock.Setup(service => service.HashPassword(input.Password)).Returns("hashedPassword");
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(user);
            _studentRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>())).ReturnsAsync(student);
            _emailServiceMock.Setup(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByCPFAsync(It.IsAny<string>()), Times.Once);
            _courseRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _campusRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _hashServiceMock.Verify(service => service.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _studentRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_NullPassword_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var user = UserMock.MockValidUser();
            var student = StudentMock.MockValidStudent();
            input.Password = null;

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(input.Email)).ReturnsAsync((Domain.Entities.User)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByCPFAsync(input.CPF)).ReturnsAsync((Domain.Entities.User)null);
            _courseRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CourseId)).ReturnsAsync(CourseMock.MockValidCourse());
            _campusRepositoryMock.Setup(repo => repo.GetByIdAsync(input.CampusId)).ReturnsAsync(CampusMock.MockValidCampus());
            _hashServiceMock.Setup(service => service.HashPassword(input.Password)).Returns("hashedPassword");
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>())).ReturnsAsync(user);
            _studentRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>())).ReturnsAsync(student);
            _emailServiceMock.Setup(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByCPFAsync(It.IsAny<string>()), Times.Once);
            _courseRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _campusRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _hashServiceMock.Verify(service => service.HashPassword(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
            _studentRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Student>()), Times.Never);
            _emailServiceMock.Verify(service => service.SendConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Never);
        }
    }
}
