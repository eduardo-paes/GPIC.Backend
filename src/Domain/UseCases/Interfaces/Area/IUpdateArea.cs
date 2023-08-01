using Domain.Ports.Area;
using Domain.UseCases.Ports.Area;

namespace Domain.UseCases.Interfaces.Area
{
    public interface IUpdateArea
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id, UpdateAreaInput input);
    }
}