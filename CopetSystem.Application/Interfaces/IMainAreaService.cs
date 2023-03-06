using System;
using CopetSystem.Application.DTOs.MainArea;
using CopetSystem.Application.Interfaces.Primitives;

namespace CopetSystem.Application.Interfaces
{
	public interface IMainAreaService
    {
        Task<ReadMainAreaDTO> GetById(Guid? id);
        Task<IQueryable<ReadMainAreaDTO>> GetAll(int skip, int take);

        Task<ReadMainAreaDTO> Create(CreateMainAreaDTO model);
        Task<ReadMainAreaDTO> Update(Guid? id, UpdateMainAreaDTO model);
        Task<ReadMainAreaDTO> Delete(Guid id);
    }
}

