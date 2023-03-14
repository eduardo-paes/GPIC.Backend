using Application.DTOs.Area;

namespace Application.Proxies.Area
{
    public interface IDeleteArea
    {
        Task<DetailedReadAreaDTO> Execute(Guid id);
    }
}