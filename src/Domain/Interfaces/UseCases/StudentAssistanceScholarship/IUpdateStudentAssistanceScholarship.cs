using Domain.Contracts.StudentAssistanceScholarship;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateStudentAssistanceScholarship
    {
        Task<DetailedReadStudentAssistanceScholarshipOutput> Execute(Guid? id, UpdateStudentAssistanceScholarshipInput model);
    }
}