using System;
using CopetSystem.Application.DTOs.MainArea;
using CopetSystem.Application.DTOs.User;

namespace CopetSystem.Application.Interfaces
{
	public interface IMainAreaService
	{
        Task<ReadMainAreaDTO> GetById(Guid? id);
        Task<IQueryable<ReadMainAreaDTO>> GetAll();

        Task<ReadMainAreaDTO> Create(CreateMainAreaDTO model);
        Task<ReadMainAreaDTO> Update(Guid? id, UpdateMainAreaDTO model);
        Task<ReadMainAreaDTO> Delete(Guid id);
    }
}

