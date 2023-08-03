using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface ICampusRepository : IGenericCRUDRepository<Campus>
    {
        Task<Campus?> GetCampusByNameAsync(string name);
    }
}