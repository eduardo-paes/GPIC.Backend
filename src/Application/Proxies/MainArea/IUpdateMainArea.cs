using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IUpdateMainArea
    {
        Task<DetailedMainAreaDTO> Execute(Guid? id, UpdateMainAreaDTO model);
    }
}