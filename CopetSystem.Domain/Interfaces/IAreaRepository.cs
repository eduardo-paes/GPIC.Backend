using System;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
	public interface IAreaRepository : IGenericCRUDRepository<Area>
    {
        Task<Area> GetByCode(string? code);
    }
}

