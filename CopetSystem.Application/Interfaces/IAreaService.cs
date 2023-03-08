using CopetSystem.Application.DTOs.Area;

namespace CopetSystem.Application.Interfaces
{
  public interface IAreaService
  {
    Task<DetailedReadAreaDTO> GetById(Guid? id);
    Task<IQueryable<ResumedReadAreaDTO>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);

    Task<DetailedReadAreaDTO> Create(CreateAreaDTO model);
    Task<DetailedReadAreaDTO> Update(Guid? id, UpdateAreaDTO model);
    Task<DetailedReadAreaDTO> Delete(Guid id);
  }
}

