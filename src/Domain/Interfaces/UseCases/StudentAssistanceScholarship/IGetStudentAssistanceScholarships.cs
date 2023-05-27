using Domain.Contracts.StudentAssistanceScholarship;

namespace Domain.Interfaces.UseCases
{
    public interface IGetStudentAssistanceScholarships
    {
        Task<IQueryable<ResumedReadStudentAssistanceScholarshipOutput>> Execute(int skip, int take);
    }
}