using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases.Student
{
    public interface IDeleteStudent
    {
        Task<DetailedReadStudentOutput> Execute(Guid? id);
    }
}