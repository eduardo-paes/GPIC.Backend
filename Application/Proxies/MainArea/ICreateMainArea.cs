using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface ICreateMainArea
    {
        Task<ReadMainAreaDTO> Execute(CreateMainAreaDTO model);
    }
}