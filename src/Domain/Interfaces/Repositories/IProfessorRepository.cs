using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IProfessorRepository : IGenericCRUDRepository<Professor>
    {
        Task<IEnumerable<Professor>> GetAllActiveProfessorsAsync();
        Task<Professor?> GetByUserIdAsync(Guid? userId);
    }
}