using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interfaces.Student
{
    public interface IGetStudentById
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id);
    }
}