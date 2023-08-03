using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IStudentDocumentsRepository : IGenericCRUDRepository<StudentDocuments>
    {
        Task<StudentDocuments?> GetByProjectIdAsync(Guid? projectId);
        Task<StudentDocuments?> GetByStudentIdAsync(Guid? studentId);
    }
}