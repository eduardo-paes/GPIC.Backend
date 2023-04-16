using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases.Student
{
    public interface ICreateStudent
    {
        Task<DetailedReadStudentOutput> Execute(CreateStudentInput model);
    }
}