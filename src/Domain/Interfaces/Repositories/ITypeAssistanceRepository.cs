using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface ITypeAssistanceRepository : IGenericCRUDRepository<TypeAssistance>
    {
        Task<TypeAssistance?> GetTypeAssistanceByName(string name);
    }
}