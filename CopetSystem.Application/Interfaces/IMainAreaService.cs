using CopetSystem.Application.DTOs.MainArea;

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

