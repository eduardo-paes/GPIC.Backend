using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Entities
{
    public class CourseUnitTests
    {
        private Course MockValidCourse() => new Course("Course Name");

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var course = MockValidCourse();
            var name = "Course Name";

            // Act
            course.Name = name;

            // Assert
            course.Name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullOrEmptyName_ThrowsException()
        {
            // Arrange
            var course = MockValidCourse();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => course.Name = null);
            Assert.Throws<EntityExceptionValidation>(() => course.Name = string.Empty);
        }

        [Fact]
        public void SetName_TooShortName_ThrowsException()
        {
            // Arrange
            var course = MockValidCourse();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => course.Name = "AB");
        }

        [Fact]
        public void SetName_TooLongName_ThrowsException()
        {
            // Arrange
            var course = MockValidCourse();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => course.Name = "lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper.");
        }
    }
}
