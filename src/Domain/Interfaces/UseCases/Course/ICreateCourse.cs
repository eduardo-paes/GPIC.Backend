using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateCourse
    {
        Task<DetailedReadCourseOutput> Execute(CreateCourseInput model);
    }
}