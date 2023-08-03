using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface ISubAreaRepository : IGenericCRUDRepository<SubArea>
    {
        Task<SubArea?> GetByCodeAsync(string? code);
        Task<IEnumerable<SubArea>> GetSubAreasByAreaAsync(Guid? areaId, int skip, int take);
    }
}