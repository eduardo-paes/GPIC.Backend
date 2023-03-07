using CopetSystem.Application.DTOs.SubArea;

namespace CopetSystem.Application.Interfaces
{
    public interface ISubAreaService
    {
        Task<ReadSubAreaDTO> GetById(Guid? id);
        Task<IQueryable<ReadSubAreaDTO>> GetAll(int skip, int take);

        Task<ReadSubAreaDTO> Create(CreateSubAreaDTO model);
        Task<ReadSubAreaDTO> Update(Guid? id, UpdateSubAreaDTO model);
        Task<ReadSubAreaDTO> Delete(Guid id);
    }
}

