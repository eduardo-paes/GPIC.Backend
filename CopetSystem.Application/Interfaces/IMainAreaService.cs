using System;
using CopetSystem.Application.DTOs.MainArea;
using CopetSystem.Application.DTOs.User;

namespace CopetSystem.Application.Interfaces
{
	public interface IMainAreaService
	{
        Task<MainAreaDTO> GetById(Guid? id);
        Task<IQueryable<MainAreaDTO>> GetAll();

        Task<MainAreaDTO> Create(MainAreaDTO model);
        Task<MainAreaDTO> Update(Guid? id, MainAreaDTO model);
        Task<MainAreaDTO> Delete(Guid id);
    }
}

