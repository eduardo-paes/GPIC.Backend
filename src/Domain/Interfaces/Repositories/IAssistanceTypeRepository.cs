using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IAssistanceTypeRepository : IGenericCRUDRepository<AssistanceType>
    {
        Task<AssistanceType?> GetAssistanceTypeByNameAsync(string name);
    }
}