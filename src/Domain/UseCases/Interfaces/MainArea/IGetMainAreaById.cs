using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interfaces.MainArea
{
    public interface IGetMainAreaById
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id);
    }
}