using Domain.UseCases.Ports.Course;

namespace Domain.UseCases.Interfaces.Course
{
    public interface IDeleteCourse
    {
        Task<DetailedReadCourseOutput> Execute(Guid? id);
    }
}