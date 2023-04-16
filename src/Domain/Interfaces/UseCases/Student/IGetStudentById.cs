using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases.Student
{
    public interface IGetStudentById
    {
        Task<DetailedReadStudentOutput> Execute(Guid? id);
    }
}