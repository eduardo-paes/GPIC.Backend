using Application.Ports.Course;

namespace Application.Interfaces.UseCases.Course
{
    public interface IDeleteCourse
    {
        Task<DetailedReadCourseOutput> ExecuteAsync(Guid? id);
    }
}