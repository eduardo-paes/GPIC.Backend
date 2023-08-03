using Domain.UseCases.Ports.Course;

namespace Domain.UseCases.Interfaces.Course
{
    public interface IUpdateCourse
    {
        Task<DetailedReadCourseOutput> ExecuteAsync(Guid? id, UpdateCourseInput input);
    }
}