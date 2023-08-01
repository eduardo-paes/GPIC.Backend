using Domain.Ports.Course;
using Domain.UseCases.Ports.Course;

namespace Domain.UseCases.Interfaces.Course
{
    public interface ICreateCourse
    {
        Task<DetailedReadCourseOutput> Execute(CreateCourseInput model);
    }
}