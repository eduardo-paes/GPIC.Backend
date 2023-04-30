using Domain.Contracts.Course;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteCourse
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id);
    }
}