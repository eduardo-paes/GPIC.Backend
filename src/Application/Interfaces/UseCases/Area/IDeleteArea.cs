using Application.Ports.Area;

namespace Application.Interfaces.UseCases.Area
{
    public interface IDeleteArea
    {
        Task<DetailedReadAreaOutput> ExecuteAsync(Guid? id);
    }
}