using System;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
	public interface IAreaRepository
	{
        Task<Area> Get();
        Task<IEnumerable<Area>> GetAll();

        Task<Area> Create(Area area);
		Task<Area> Remove(Area area);
		Task<Area> Update(Area area);
    }
}

