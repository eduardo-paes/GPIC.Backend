using Application.Ports.Course;

namespace Application.Interfaces.UseCases.Course
{
    public interface IGetCourses
    {
        Task<IQueryable<ResumedReadCourseOutput>> ExecuteAsync(int skip, int take);
    }
}