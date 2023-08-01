using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interfaces.Student
{
    public interface IUpdateStudent
    {
        Task<DetailedReadStudentOutput> Execute(Guid? id, UpdateStudentInput model);
    }
}