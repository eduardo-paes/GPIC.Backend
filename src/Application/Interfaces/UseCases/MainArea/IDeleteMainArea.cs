using Application.Ports.MainArea;

namespace Application.Interfaces.UseCases.MainArea
{
    public interface IDeleteMainArea
    {
        Task<DetailedMainAreaOutput> ExecuteAsync(Guid? id);
    }
}