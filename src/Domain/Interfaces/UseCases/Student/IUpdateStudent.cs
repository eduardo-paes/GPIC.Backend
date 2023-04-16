using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases.Student
{
    public interface IUpdateStudent
    {
        Task<DetailedReadStudentOutput> Execute(Guid? id, UpdateStudentInput model);
    }
}