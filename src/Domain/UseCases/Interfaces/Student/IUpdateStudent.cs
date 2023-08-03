using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interfaces.Student
{
    public interface IUpdateStudent
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id, UpdateStudentInput model);
    }
}