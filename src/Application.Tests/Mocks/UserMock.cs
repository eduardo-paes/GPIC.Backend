using Domain.Entities.Enums;

namespace Application.Tests.Mocks
{
    public static class UserMock
    {
        public static Domain.Entities.User MockValidUser() => new(Guid.NewGuid(), "John Doe", "john.doe@example.com", "strongpassword", "92114660087", ERole.ADMIN);

    }
}