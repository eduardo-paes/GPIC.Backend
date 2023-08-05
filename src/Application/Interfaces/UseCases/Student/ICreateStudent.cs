using Application.Ports.Student;

namespace Application.Interfaces.UseCases.Student
{
    public interface ICreateStudent
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(CreateStudentInput model);
    }
}