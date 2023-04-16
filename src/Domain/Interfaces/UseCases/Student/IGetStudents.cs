using Domain.Contracts.Student;

namespace Domain.Interfaces.UseCases.Student
{
    public interface IGetStudents
    {
        Task<IQueryable<ResumedReadStudentOutput>> Execute(int skip, int take);
    }
}