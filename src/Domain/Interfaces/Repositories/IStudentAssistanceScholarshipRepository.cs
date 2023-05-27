using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IStudentAssistanceScholarshipRepository : IGenericCRUDRepository<StudentAssistanceScholarship>
    {
        Task<StudentAssistanceScholarship?> GetStudentAssistanceScholarshipByName(string name);
    }
}