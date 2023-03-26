using Domain.Contracts.Area;

namespace Domain.Interfaces.Area
{
    public interface IGetAreaById
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id);
    }
}