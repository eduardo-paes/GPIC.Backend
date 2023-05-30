using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Entities
{
    public class CampusTests
    {
        private Campus MockValidCampus() => new Campus("Campus Name");

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var campus = MockValidCampus();
            var name = "Campus Name";

            // Act
            campus.Name = name;

            // Assert
            campus.Name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullOrEmptyName_ThrowsException()
        {
            // Arrange
            var campus = MockValidCampus();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => campus.Name = null);
            Assert.Throws<EntityExceptionValidation>(() => campus.Name = string.Empty);
        }

        [Fact]
        public void SetName_TooShortName_ThrowsException()
        {
            // Arrange
            var campus = MockValidCampus();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => campus.Name = "AB");
        }

        [Fact]
        public void SetName_TooLongName_ThrowsException()
        {
            // Arrange
            var campus = MockValidCampus();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => campus.Name = new string('A', 1500));
        }
    }
}
