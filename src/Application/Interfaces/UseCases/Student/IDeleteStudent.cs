using Application.Ports.Student;

namespace Application.Interfaces.UseCases.Student
{
    public interface IDeleteStudent
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id);
    }
}