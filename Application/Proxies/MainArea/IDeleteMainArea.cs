using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IDeleteMainArea
    {
        Task<ReadMainAreaDTO> Execute(Guid id);
    }
}