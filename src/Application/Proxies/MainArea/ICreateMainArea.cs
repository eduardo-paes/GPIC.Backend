using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface ICreateMainArea
    {
        Task<DetailedMainAreaDTO> Execute(CreateMainAreaDTO model);
    }
}