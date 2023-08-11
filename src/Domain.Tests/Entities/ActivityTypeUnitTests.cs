using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ActivityTypeUnitTests
    {
        private static ActivityType MockValidActivityType() =>
            new("Valid Name", "Valid Unity", Guid.NewGuid());

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var activityType = MockValidActivityType();
            var name = "Valid New Name";

            // Act
            activityType.Name = name;

            // Assert
            activityType.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("AB")]
        [InlineData("A")]
        [InlineData(null)]
        public void SetName_InvalidName_ThrowsException(string invalidName)
        {
            // Arrange
            var activityType = MockValidActivityType();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => activityType.Name = invalidName);
        }

        [Fact]
        public void SetUnity_ValidUnity_SetsUnity()
        {
            // Arrange
            var activityType = MockValidActivityType();
            var unity = "Valid New Unity";

            // Act
            activityType.Unity = unity;

            // Assert
            activityType.Unity.Should().Be(unity);
        }

        [Theory]
        [InlineData("")]
        [InlineData("AB")]
        [InlineData("A")]
        [InlineData(null)]
        public void SetUnity_InvalidUnity_ThrowsException(string invalidUnity)
        {
            // Arrange
            var activityType = MockValidActivityType();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => activityType.Unity = invalidUnity);
        }

        [Fact]
        public void Constructor_WithValidArguments_CreatesActivityTypeWithCorrectValues()
        {
            // Arrange
            var name = "Valid Name";
            var unity = "Valid Unity";
            var noticeId = Guid.NewGuid();

            // Act
            var activityType = new ActivityType(name, unity, noticeId);

            // Assert
            activityType.Name.Should().Be(name);
            activityType.Unity.Should().Be(unity);
            activityType.NoticeId.Should().Be(noticeId);
        }

        [Theory]
        [InlineData("")]
        [InlineData("AB")]
        [InlineData("A")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_ThrowsException(string invalidName)
        {
            // Arrange
            var unity = "Valid Unity";
            var noticeId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new ActivityType(invalidName, unity, noticeId));
        }

        [Theory]
        [InlineData("")]
        [InlineData("AB")]
        [InlineData("A")]
        [InlineData(null)]
        public void Constructor_WithInvalidUnity_ThrowsException(string invalidUnity)
        {
            // Arrange
            var name = "Valid Name";
            var noticeId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new ActivityType(name, invalidUnity, noticeId));
        }

        [Fact]
        public void Constructor_WithNullNoticeId_ThrowsException()
        {
            // Arrange
            var name = "Valid Name";
            var unity = "Valid Unity";
            var noticeId = (Guid?)null;

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new ActivityType(name, unity, noticeId));
        }
    }
}
