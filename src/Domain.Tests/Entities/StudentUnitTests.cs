using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.Tests.Entities
{
    public class StudentUnitTests
    {
        private Student MockValidStudent() => new Student(
            birthDate: new DateTime(2000, 1, 1),
            rg: 123456789,
            issuingAgency: "Agency",
            dispatchDate: new DateTime(2020, 1, 1),
            gender: EGender.M,
            race: ERace.White,
            homeAddress: "Street 1",
            city: "City",
            uf: "UF",
            cep: 12345678,
            phoneDDD: 11,
            phone: 12345678,
            cellPhoneDDD: 22,
            cellPhone: 987654321,
            campusId: Guid.NewGuid(),
            courseId: Guid.NewGuid(),
            startYear: "2022",
            studentAssistanceProgramId: Guid.NewGuid()
        );

        [Fact]
        public void SetBirthDate_ValidDate_SetsBirthDate()
        {
            // Arrange
            var student = MockValidStudent();
            var birthDate = DateTime.UtcNow.AddDays(-1);

            // Act
            student.BirthDate = birthDate;

            // Assert
            student.BirthDate.Should().Be(birthDate);
        }

        [Fact]
        public void SetBirthDate_DefaultDate_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.BirthDate = default);
        }

        [Fact]
        public void SetBirthDate_FutureDate_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.BirthDate = DateTime.UtcNow.AddDays(1));
        }

        [Fact]
        public void SetRG_ValidRG_SetsRG()
        {
            // Arrange
            var student = MockValidStudent();
            var rg = 123456789;

            // Act
            student.RG = rg;

            // Assert
            student.RG.Should().Be(rg);
        }

        [Fact]
        public void SetRG_NonPositiveRG_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.RG = 0);
        }

        [Fact]
        public void SetIssuingAgency_ValidIssuingAgency_SetsIssuingAgency()
        {
            // Arrange
            var student = MockValidStudent();
            var issuingAgency = "IssuingAgency";

            // Act
            student.IssuingAgency = issuingAgency;

            // Assert
            student.IssuingAgency.Should().Be(issuingAgency);
        }

        [Fact]
        public void SetIssuingAgency_NullOrEmptyIssuingAgency_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.IssuingAgency = null);
            Assert.Throws<EntityExceptionValidation>(() => student.IssuingAgency = string.Empty);
        }

        [Fact]
        public void SetIssuingAgency_LongIssuingAgency_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.IssuingAgency = new string('A', 51));
        }

        [Fact]
        public void SetDispatchDate_ValidDate_SetsDispatchDate()
        {
            // Arrange
            var student = MockValidStudent();
            var dispatchDate = DateTime.UtcNow.AddDays(-1);

            // Act
            student.DispatchDate = dispatchDate;

            // Assert
            student.DispatchDate.Should().Be(dispatchDate);
        }

        [Fact]
        public void SetDispatchDate_DefaultDate_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.DispatchDate = default);
        }

        [Fact]
        public void SetDispatchDate_FutureDate_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.DispatchDate = DateTime.UtcNow.AddDays(1));
        }

        [Fact]
        public void SetGender_ValidGender_SetsGender()
        {
            // Arrange
            var student = MockValidStudent();
            var gender = EGender.F;

            // Act
            student.Gender = gender;

            // Assert
            student.Gender.Should().Be(gender);
        }

        [Fact]
        public void SetRace_ValidRace_SetsRace()
        {
            // Arrange
            var student = MockValidStudent();
            var race = ERace.White;

            // Act
            student.Race = race;

            // Assert
            student.Race.Should().Be(race);
        }

        [Fact]
        public void SetHomeAddress_ValidAddress_SetsHomeAddress()
        {
            // Arrange
            var student = MockValidStudent();
            var homeAddress = "HomeAddress";

            // Act
            student.HomeAddress = homeAddress;

            // Assert
            student.HomeAddress.Should().Be(homeAddress);
        }

        [Fact]
        public void SetHomeAddress_NullOrEmptyAddress_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.HomeAddress = null);
            Assert.Throws<EntityExceptionValidation>(() => student.HomeAddress = string.Empty);
        }

        [Fact]
        public void SetHomeAddress_LongAddress_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.HomeAddress = new string('A', 500));
        }

        [Fact]
        public void SetCity_ValidCity_SetsCity()
        {
            // Arrange
            var student = MockValidStudent();
            var city = "City";

            // Act
            student.City = city;

            // Assert
            student.City.Should().Be(city);
        }

        [Fact]
        public void SetCity_NullOrEmptyCity_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.City = null);
            Assert.Throws<EntityExceptionValidation>(() => student.City = string.Empty);
        }

        [Fact]
        public void SetCity_LongCity_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.City = new string('A', 51));
        }

        [Fact]
        public void SetUF_ValidUF_SetsUF()
        {
            // Arrange
            var student = MockValidStudent();
            var uf = "UF";

            // Act
            student.UF = uf;

            // Assert
            student.UF.Should().Be(uf);
        }

        [Fact]
        public void SetUF_NullOrEmptyUF_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.UF = null);
            Assert.Throws<EntityExceptionValidation>(() => student.UF = string.Empty);
        }

        [Fact]
        public void SetUF_LongUF_ThrowsException()
        {
            // Arrange
            var student = MockValidStudent();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => student.UF = new string('A', 3));
        }
    }
}
