using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IStudentDocumentsRepository : IGenericCRUDRepository<StudentDocuments>
    {
        Task<StudentDocuments?> GetByProjectId(Guid? projectId);
        Task<StudentDocuments?> GetByStudentId(Guid? studentId);
    }
}