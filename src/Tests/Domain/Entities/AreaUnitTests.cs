using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Entities;
public class AreaUnitTests
{
    private Area MockValidArea() => new Area(Guid.NewGuid(), "ABC", "Area Name");

    [Fact]
    public void SetMainAreaId_ValidId_SetsMainAreaId()
    {
        // Arrange
        var area = MockValidArea();
        var mainAreaId = Guid.NewGuid();

        // Act
        area.MainAreaId = mainAreaId;

        // Assert
        area.MainAreaId.Should().Be(mainAreaId);
    }

    [Fact]
    public void SetMainAreaId_NullId_ThrowsException()
    {
        // Arrange
        var area = MockValidArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => area.MainAreaId = null);
    }

    [Fact]
    public void SetCode_ValidCode_SetsCode()
    {
        // Arrange
        var area = MockValidArea();
        var code = "ABC";

        // Act
        area.Code = code;

        // Assert
        area.Code.Should().Be(code);
    }

    [Fact]
    public void SetCode_NullOrEmptyCode_ThrowsException()
    {
        Action action = () => new Area(Guid.NewGuid(), null, "Area Name");
        action.Should()
            .Throw<EntityExceptionValidation>();
    }

    [Fact]
    public void SetCode_TooShortCode_ThrowsException()
    {
        // Arrange
        var area = MockValidArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => area.Code = "AB");
    }

    [Fact]
    public void SetCode_TooLongCode_ThrowsException()
    {
        // Arrange
        var area = MockValidArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => area.Code = "lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper.");
    }

    [Fact]
    public void SetName_ValidName_SetsName()
    {
        // Arrange
        var area = MockValidArea();
        var name = "Area Name";

        // Act
        area.Name = name;

        // Assert
        area.Name.Should().Be(name);
    }

    [Fact]
    public void SetName_NullOrEmptyName_ThrowsException()
    {
        // Arrange
        var area = MockValidArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => area.Name = null);
        Assert.Throws<EntityExceptionValidation>(() => area.Name = string.Empty);
    }

    [Fact]
    public void SetName_TooShortName_ThrowsException()
    {
        // Arrange
        var area = MockValidArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => area.Name = "AB");
    }

    [Fact]
    public void SetName_TooLongName_ThrowsException()
    {
        // Arrange
        var area = MockValidArea();

        // Act & Assert
        Assert.Throws<EntityExceptionValidation>(() => area.Name = "lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper. lorem ipsum dolor sit amet, consectetur adipiscing elit. mauris ullamcorper.");
    }
}