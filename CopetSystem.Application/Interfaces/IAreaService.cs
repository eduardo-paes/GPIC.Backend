using CopetSystem.Application.DTOs.Area;

namespace CopetSystem.Application.Interfaces
{
    public interface IAreaService
    {
        Task<ReadAreaDTO> GetById(Guid? id);
        Task<IQueryable<ReadAreaDTO>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);

        Task<ReadAreaDTO> Create(CreateAreaDTO model);
        Task<ReadAreaDTO> Update(Guid? id, UpdateAreaDTO model);
        Task<ReadAreaDTO> Delete(Guid id);
    }
}

