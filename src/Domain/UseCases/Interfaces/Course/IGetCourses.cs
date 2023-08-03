using Domain.UseCases.Ports.Course;

namespace Domain.UseCases.Interfaces.Course
{
    public interface IGetCourses
    {
        Task<IQueryable<ResumedReadCourseOutput>> ExecuteAsync(int skip, int take);
    }
}