using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class CourseMock
    {
        public static Course MockValidCourse() => new("Course Name");
    }
}