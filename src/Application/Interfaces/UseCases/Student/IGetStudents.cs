using Application.Ports.Student;

namespace Application.Interfaces.UseCases.Student
{
    public interface IGetStudents
    {
        Task<IQueryable<ResumedReadStudentOutput>> ExecuteAsync(int skip, int take);
    }
}