namespace Application.Tests.Mocks
{
    public static class ClaimsMock
    {
        public static Dictionary<Guid, Domain.Entities.User> MockValidClaims() => new() { { Guid.NewGuid(), UserMock.MockValidUser() } };
    }
}