using CopetSystem.Domain.Entities;
using FluentAssertions;

namespace CopetSystem.Domain.Tests;
public class UserUnitTests
{
    [Fact(DisplayName = "Create User With Valid State")]
    public void CreateUser_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new User("User Name", "username@gmail.com", "123456", "15162901784", "TEST");
        action.Should()
             .NotThrow<CopetSystem.Domain.Validation.DomainExceptionValidation>();
    }
}

