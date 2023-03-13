using System;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMainAreaRepository : IGenericCRUDRepository<MainArea>
    {
        Task<MainArea?> GetByCode(string? code);
    }
}

