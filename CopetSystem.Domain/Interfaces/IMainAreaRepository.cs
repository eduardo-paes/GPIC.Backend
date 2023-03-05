using System;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
	public interface IMainAreaRepository : IGenericCRUDRepository<MainArea>
	{
        Task<MainArea?> GetByCode(string? code);
    }
}

