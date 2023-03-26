using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IMainAreaRepository : IGenericCRUDRepository<MainArea>
    {
        Task<MainArea?> GetByCode(string? code);
    }
}

