using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases.Course
{
    public interface IGetCourses
    {
        Task<IQueryable<ResumedReadCourseOutput>> Execute(int skip, int take);
    }
}