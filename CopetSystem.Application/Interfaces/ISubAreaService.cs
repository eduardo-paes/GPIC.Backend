using CopetSystem.Application.DTOs.SubArea;

namespace CopetSystem.Application.Interfaces
{
    public interface ISubAreaService
    {
        Task<DetailedReadSubAreaDTO> GetById(Guid? id);
        Task<IQueryable<ResumedReadSubAreaDTO>> GetSubAreasByArea(Guid? areaId, int skip, int take);

        Task<DetailedReadSubAreaDTO> Create(CreateSubAreaDTO model);
        Task<DetailedReadSubAreaDTO> Update(Guid? id, UpdateSubAreaDTO model);
        Task<DetailedReadSubAreaDTO> Delete(Guid id);
    }
}