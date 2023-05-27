using Domain.Contracts.StudentAssistanceScholarship;

namespace Domain.Interfaces.UseCases
{
    public interface IGetStudentAssistanceScholarshipById
    {
        Task<DetailedReadStudentAssistanceScholarshipOutput> Execute(Guid? id);
    }
}