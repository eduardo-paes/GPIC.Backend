using Application.DTOs.Area;

namespace Application.Proxies.Area
{
    public interface ICreateArea
    {
        Task<DetailedReadAreaDTO> Execute(CreateAreaDTO model);
    }
}