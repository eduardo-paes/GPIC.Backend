using Domain.UseCases.Ports.Course;

namespace Domain.UseCases.Interfaces.Course
{
    public interface IUpdateCourse
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id, UpdateCourseInput input);
    }
}