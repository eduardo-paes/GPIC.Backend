using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class CampusMock
    {
        public static Campus MockValidCampus() => new("Campus Name");
    }
}