using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IGetMainAreaById
    {
        Task<ReadMainAreaDTO> Execute(Guid? id);
    }
}