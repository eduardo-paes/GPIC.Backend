using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IProgramTypeRepository : IGenericCRUDRepository<ProgramType>
    {
        Task<ProgramType?> GetProgramTypeByName(string name);
    }
}