using Application.DTOs.Area;

namespace Application.Proxies.Area
{
    public interface IGetAreaById
    {
        Task<DetailedReadAreaDTO> Execute(Guid? id);
    }
}