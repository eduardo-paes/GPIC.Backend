using Application.Ports.Course;

namespace Application.Interfaces.UseCases.Course
{
    public interface IUpdateCourse
    {
        Task<DetailedReadCourseOutput> ExecuteAsync(Guid? id, UpdateCourseInput input);
    }
}