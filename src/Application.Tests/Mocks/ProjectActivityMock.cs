using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class ProjectActivityMock
    {
        public static ProjectActivity MockValidProjectActivity() => new(Guid.NewGuid(), Guid.NewGuid(), 5, 3);
    }
}