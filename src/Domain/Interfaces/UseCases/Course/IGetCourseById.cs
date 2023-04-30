using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases
{
    public interface IGetCourseById
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id);
    }
}