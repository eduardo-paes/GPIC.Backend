using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases
{
    public interface IGetCourses
    {
        Task<IQueryable<ResumedReadCourseOutput>> Execute(int skip, int take);
    }
}