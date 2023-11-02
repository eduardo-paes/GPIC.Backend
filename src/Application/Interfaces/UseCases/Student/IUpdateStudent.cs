using Application.Ports.Student;

namespace Application.Interfaces.UseCases.Student
{
    public interface IUpdateStudent
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id, UpdateStudentInput model);
    }
}