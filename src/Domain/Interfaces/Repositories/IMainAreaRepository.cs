using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IMainAreaRepository : IGenericCRUDRepository<MainArea>
    {
        Task<MainArea?> GetByCodeAsync(string? code);
    }
}