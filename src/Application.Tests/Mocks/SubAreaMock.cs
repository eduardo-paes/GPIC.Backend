using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class SubAreaMock
    {
        public static SubArea MockValidSubArea() => new(Guid.NewGuid(), "ABC", "SubArea Name");
    }
}