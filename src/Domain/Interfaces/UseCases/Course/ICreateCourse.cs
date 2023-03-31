using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases.Course
{
    public interface ICreateCourse
    {
        Task<DetailedReadCourseOutput> Execute(CreateCourseInput model);
    }
}