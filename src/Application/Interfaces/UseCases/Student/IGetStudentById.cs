using Application.Ports.Student;

namespace Application.Interfaces.UseCases.Student
{
    public interface IGetStudentById
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id);
    }
}