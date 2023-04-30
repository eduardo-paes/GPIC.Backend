using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases
{
    public interface IGetMainAreaById
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id);
    }
}