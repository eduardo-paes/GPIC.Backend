using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases.Course
{
    public interface IGetCourseById
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id);
    }
}