using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateStudent
    {
        Task<DetailedReadStudentOutput> Execute(Guid? id, UpdateStudentInput model);
    }
}