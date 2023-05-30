using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities;
public class MainAreaUnitTests
{
    private MainArea MockValidMainArea() => new MainArea("ABC", "Main Area Name");

    [Fact]
    public void SetCode_ValidCode_SetsCode()
    {
        // Arrange
        var mainArea = MockValidMainArea();
        var code = "ABC";

        // Act
        mainArea.Code = code;

        // Assert
        mainArea.Code.Should().Be(code);
    }

    [Fact]
    public void SetCode_NullOrEmptyCode_ThrowsException()
    {
        // Arrange
        var mainArea = MockValidMainArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Code = null);
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Code = string.Empty);
    }

    [Fact]
    public void SetCode_TooShortCode_ThrowsException()
    {
        // Arrange
        var mainArea = MockValidMainArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Code = "AB");
    }

    [Fact]
    public void SetCode_TooLongCode_ThrowsException()
    {
        // Arrange
        var mainArea = MockValidMainArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Code = new string('A', 1500));
    }

    [Fact]
    public void SetName_ValidName_SetsName()
    {
        // Arrange
        var mainArea = MockValidMainArea();
        var name = "Main Area Name";

        // Act
        mainArea.Name = name;

        // Assert
        mainArea.Name.Should().Be(name);
    }

    [Fact]
    public void SetName_NullOrEmptyName_ThrowsException()
    {
        // Arrange
        var mainArea = MockValidMainArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Name = null);
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Name = string.Empty);
    }

    [Fact]
    public void SetName_TooShortName_ThrowsException()
    {
        // Arrange
        var mainArea = MockValidMainArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Name = "AB");
    }

    [Fact]
    public void SetName_TooLongName_ThrowsException()
    {
        // Arrange
        var mainArea = MockValidMainArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => mainArea.Name = new string('A', 1500));
    }
}