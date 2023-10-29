using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class SubAreaUnitTests
    {
        private SubArea MockValidSubArea() => new SubArea(Guid.NewGuid(), "ABC", "SubArea Name");

        [Fact]
        public void SetAreaId_ValidAreaId_SetsAreaId()
        {
            // Arrange
            var subArea = MockValidSubArea();
            var areaId = Guid.NewGuid();

            // Act
            subArea.AreaId = areaId;

            // Assert
            subArea.AreaId.Should().Be(areaId);
        }

        [Fact]
        public void SetAreaId_NullAreaId_ThrowsException()
        {
            // Arrange
            var subArea = MockValidSubArea();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => subArea.AreaId = null);
        }

        [Fact]
        public void SetCode_ValidCode_SetsCode()
        {
            // Arrange
            var subArea = MockValidSubArea();
            var code = "ABC";

            // Act
            subArea.Code = code;

            // Assert
            subArea.Code.Should().Be(code);
        }

        [Fact]
        public void SetCode_NullOrEmptyCode_ThrowsException()
        {
            // Arrange
            var subArea = MockValidSubArea();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => subArea.Code = null);
            Assert.Throws<EntityExceptionValidation>(() => subArea.Code = string.Empty);
        }

        [Fact]
        public void SetCode_TooShortCode_ThrowsException()
        {
            // Arrange
            var subArea = MockValidSubArea();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => subArea.Code = "AB");
        }

        [Fact]
        public void SetCode_TooLongCode_ThrowsException()
        {
            // Arrange
            var subArea = MockValidSubArea();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => subArea.Code = new string('A', 1500));
        }

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var subArea = MockValidSubArea();
            var name = "SubArea Name";

            // Act
            subArea.Name = name;

            // Assert
            subArea.Name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullOrEmptyName_ThrowsException()
        {
            // Arrange
            var subArea = MockValidSubArea();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => subArea.Name = null);
            Assert.Throws<EntityExceptionValidation>(() => subArea.Name = string.Empty);
        }

        [Fact]
        public void SetName_TooShortName_ThrowsException()
        {
            // Arrange
            var subArea = MockValidSubArea();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => subArea.Name = "AB");
        }

        [Fact]
        public void SetName_TooLongName_ThrowsException()
        {
            // Arrange
            var subArea = MockValidSubArea();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => subArea.Name = new string('A', 1500));
        }
    }
}
