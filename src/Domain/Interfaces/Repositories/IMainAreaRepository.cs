using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IMainAreaRepository : IGenericCRUDRepository<MainArea>
    {
        Task<MainArea?> GetByCode(string? code);
    }
}