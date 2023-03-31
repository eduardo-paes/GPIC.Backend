using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases.Course
{
    public interface IDeleteCourse
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id);
    }
}