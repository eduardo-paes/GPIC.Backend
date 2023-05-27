using Domain.Contracts.StudentAssistanceScholarship;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteStudentAssistanceScholarship
    {
        Task<DetailedReadStudentAssistanceScholarshipOutput> Execute(Guid? id);
    }
}