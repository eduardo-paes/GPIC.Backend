using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interfaces.Student
{
    public interface IDeleteStudent
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id);
    }
}