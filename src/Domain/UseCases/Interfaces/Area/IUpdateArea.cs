using Domain.UseCases.Ports.Area;

namespace Domain.UseCases.Interfaces.Area
{
    public interface IUpdateArea
    {
        Task<DetailedReadAreaOutput> ExecuteAsync(Guid? id, UpdateAreaInput input);
    }
}