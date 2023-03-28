using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases.MainArea
{
    public interface IGetMainAreaById
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id);
    }
}