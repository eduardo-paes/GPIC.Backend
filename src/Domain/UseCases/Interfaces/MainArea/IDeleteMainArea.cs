using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interfaces.MainArea
{
    public interface IDeleteMainArea
    {
        Task<DetailedMainAreaOutput> ExecuteAsync(Guid? id);
    }
}