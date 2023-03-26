using Domain.Contracts.Area;

namespace Domain.Interfaces.Area
{
    public interface IUpdateArea
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id, UpdateAreaInput model);
    }
}