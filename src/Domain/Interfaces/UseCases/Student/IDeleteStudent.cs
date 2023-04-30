using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteStudent
    {
        Task<DetailedReadStudentOutput> Execute(Guid? id);
    }
}