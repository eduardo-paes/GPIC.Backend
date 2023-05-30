using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Tests.Domain.Entities
{
    public class ProgramTypeUnitTests
    {
        private ProgramType MockValidProgramType() => new ProgramType();

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var programType = MockValidProgramType();
            var name = "Program Type Name";

            // Act
            programType.Name = name;

            // Assert
            programType.Name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullOrEmptyName_ThrowsException()
        {
            // Arrange
            var programType = MockValidProgramType();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => programType.Name = null);
            Assert.Throws<EntityExceptionValidation>(() => programType.Name = string.Empty);
        }

        [Fact]
        public void SetName_TooShortName_ThrowsException()
        {
            // Arrange
            var programType = MockValidProgramType();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => programType.Name = "AB");
        }

        [Fact]
        public void SetName_TooLongName_ThrowsException()
        {
            // Arrange
            var programType = MockValidProgramType();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => programType.Name = new string('A', 1500));
        }

        [Fact]
        public void SetDescription_ValidDescription_SetsDescription()
        {
            // Arrange
            var programType = MockValidProgramType();
            var description = "Program Type Description";

            // Act
            programType.Description = description;

            // Assert
            programType.Description.Should().Be(description);
        }

        [Fact]
        public void SetDescription_NullDescription_SetsDescriptionToNull()
        {
            // Arrange
            var programType = MockValidProgramType();

            // Act
            programType.Description = null;

            // Assert
            programType.Description.Should().BeNull();
        }

        [Fact]
        public void SetDescription_TooShortDescription_ThrowsException()
        {
            // Arrange
            var programType = MockValidProgramType();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => programType.Description = "AB");
        }

        [Fact]
        public void SetDescription_TooLongDescription_ThrowsException()
        {
            // Arrange
            var programType = MockValidProgramType();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => programType.Description = new string('A', 1500));
        }
    }
}
