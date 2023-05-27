using Domain.Contracts.StudentAssistanceScholarship;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateStudentAssistanceScholarship
    {
        Task<DetailedReadStudentAssistanceScholarshipOutput> Execute(CreateStudentAssistanceScholarshipInput model);
    }
}