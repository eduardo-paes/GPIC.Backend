using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Tests.Domain.Entities
{
    public class ProfessorUnitTests
    {
        private Professor MockValidProfessor() => new Professor();

        [Fact]
        public void SetSIAPEEnrollment_ValidSIAPEEnrollment_SetsSIAPEEnrollment()
        {
            // Arrange
            var professor = MockValidProfessor();
            var siapeEnrollment = "1234567";

            // Act
            professor.SIAPEEnrollment = siapeEnrollment;

            // Assert
            professor.SIAPEEnrollment.Should().Be(siapeEnrollment);
        }

        [Fact]
        public void SetSIAPEEnrollment_NullSIAPEEnrollment_ThrowsException()
        {
            // Arrange
            var professor = MockValidProfessor();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => professor.SIAPEEnrollment = null);
        }

        [Fact]
        public void SetSIAPEEnrollment_InvalidLengthSIAPEEnrollment_ThrowsException()
        {
            // Arrange
            var professor = MockValidProfessor();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => professor.SIAPEEnrollment = "123");
        }

        [Fact]
        public void SetIdentifyLattes_ValidIdentifyLattes_SetsIdentifyLattes()
        {
            // Arrange
            var professor = MockValidProfessor();
            var identifyLattes = 12345;

            // Act
            professor.IdentifyLattes = identifyLattes;

            // Assert
            professor.IdentifyLattes.Should().Be(identifyLattes);
        }

        [Fact]
        public void SetIdentifyLattes_InvalidIdentifyLattes_ThrowsException()
        {
            // Arrange
            var professor = MockValidProfessor();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => professor.IdentifyLattes = 0);
        }

        [Fact]
        public void SetUserId_ValidUserId_SetsUserId()
        {
            // Arrange
            var professor = MockValidProfessor();
            var userId = Guid.NewGuid();

            // Act
            professor.UserId = userId;

            // Assert
            professor.UserId.Should().Be(userId);
        }

        [Fact]
        public void SetUserId_NullUserId_ThrowsException()
        {
            // Arrange
            var professor = MockValidProfessor();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => professor.UserId = null);
        }
    }
}
