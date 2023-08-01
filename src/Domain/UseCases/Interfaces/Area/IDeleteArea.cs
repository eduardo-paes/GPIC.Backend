using Domain.UseCases.Ports.Area;

namespace Domain.UseCases.Interfaces.Area
{
    public interface IDeleteArea
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id);
    }
}