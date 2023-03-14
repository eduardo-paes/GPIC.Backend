using Application.DTOs.Area;

namespace Application.Proxies.Area
{
    public interface IUpdateArea
    {
        Task<DetailedReadAreaDTO> Execute(Guid? id, UpdateAreaDTO model);
    }
}