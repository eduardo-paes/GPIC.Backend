using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateStudent
    {
        Task<DetailedReadStudentOutput> Execute(CreateStudentInput model);
    }
}