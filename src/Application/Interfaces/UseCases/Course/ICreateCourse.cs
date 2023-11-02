using Application.Ports.Course;

namespace Application.Interfaces.UseCases.Course
{
    public interface ICreateCourse
    {
        Task<DetailedReadCourseOutput> ExecuteAsync(CreateCourseInput model);
    }
}