using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interfaces.Student
{
    public interface IGetStudents
    {
        Task<IQueryable<ResumedReadStudentOutput>> ExecuteAsync(int skip, int take);
    }
}