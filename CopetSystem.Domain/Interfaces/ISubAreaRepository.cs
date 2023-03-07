using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
    public interface ISubAreaRepository : IGenericCRUDRepository<SubArea>
    {
        Task<SubArea?> GetByCode(string? code);
    }
}

