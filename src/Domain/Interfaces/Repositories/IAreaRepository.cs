using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAreaRepository : IGenericCRUDRepository<Area>
    {
        Task<Area?> GetByCode(string? code);
        Task<IEnumerable<Area>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);
    }
}