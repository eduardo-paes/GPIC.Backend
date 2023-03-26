using Domain.Contracts.Area;

namespace Domain.Interfaces.Area
{
    public interface IDeleteArea
    {
        Task<DetailedReadAreaOutput> Execute(Guid id);
    }
}