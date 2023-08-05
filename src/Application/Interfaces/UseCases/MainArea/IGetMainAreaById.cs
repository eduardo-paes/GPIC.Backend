using Application.Ports.MainArea;

namespace Application.Interfaces.UseCases.MainArea
{
    public interface IGetMainAreaById
    {
        Task<DetailedMainAreaOutput> ExecuteAsync(Guid? id);
    }
}