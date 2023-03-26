using Domain.Contracts.MainArea;

namespace Domain.Interfaces.MainArea
{
    public interface IGetMainAreaById
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id);
    }
}