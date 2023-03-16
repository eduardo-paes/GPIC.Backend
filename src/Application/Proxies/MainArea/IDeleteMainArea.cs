using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IDeleteMainArea
    {
        Task<DetailedMainAreaDTO> Execute(Guid id);
    }
}