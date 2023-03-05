using System;
using CopetSystem.Application.DTOs.Area;
using CopetSystem.Application.DTOs.User;

namespace CopetSystem.Application.Interfaces
{
	public interface IAreaService
	{
        Task<ReadAreaDTO> GetById(Guid? id);
        Task<IQueryable<ReadAreaDTO>> GetAll();

        Task<ReadAreaDTO> Create(CreateAreaDTO model);
        Task<ReadAreaDTO> Update(Guid? id, UpdateAreaDTO model);
        Task<ReadAreaDTO> Delete(Guid id);
    }
}

