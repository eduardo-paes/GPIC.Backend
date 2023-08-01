using Domain.UseCases.Ports.Course;

namespace Domain.UseCases.Interfaces.Course
{
    public interface IGetCourseById
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id);
    }
}