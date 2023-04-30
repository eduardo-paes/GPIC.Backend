using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases
{
    public interface IGetStudentById
    {
        Task<DetailedReadStudentOutput> Execute(Guid? id);
    }
}