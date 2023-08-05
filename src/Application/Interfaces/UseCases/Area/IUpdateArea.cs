using Application.Ports.Area;

namespace Application.Interfaces.UseCases.Area
{
    public interface IUpdateArea
    {
        Task<DetailedReadAreaOutput> ExecuteAsync(Guid? id, UpdateAreaInput input);
    }
}