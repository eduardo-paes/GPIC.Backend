using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IUpdateMainArea
    {
        Task<ReadMainAreaDTO> Execute(Guid? id, UpdateMainAreaDTO model);
    }
}