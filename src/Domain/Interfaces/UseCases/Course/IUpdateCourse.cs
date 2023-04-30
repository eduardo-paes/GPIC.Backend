using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateCourse
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id, UpdateCourseInput model);
    }
}