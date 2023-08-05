using Application.Ports.Course;

namespace Application.Interfaces.UseCases.Course
{
    public interface IGetCourseById
    {
        Task<DetailedReadCourseOutput> ExecuteAsync(Guid? id);
    }
}