using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IAreaRepository : IGenericCRUDRepository<Area>
    {
        Task<Area?> GetByCodeAsync(string? code);
        Task<IEnumerable<Area>> GetAreasByMainAreaAsync(Guid? mainAreaId, int skip, int take);
    }
}