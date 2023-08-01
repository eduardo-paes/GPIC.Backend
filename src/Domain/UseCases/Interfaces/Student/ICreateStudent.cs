using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interfaces.Student
{
    public interface ICreateStudent
    {
        Task<DetailedReadStudentOutput> Execute(CreateStudentInput model);
    }
}