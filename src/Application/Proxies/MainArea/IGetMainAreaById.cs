using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IGetMainAreaById
    {
        Task<DetailedMainAreaDTO> Execute(Guid? id);
    }
}