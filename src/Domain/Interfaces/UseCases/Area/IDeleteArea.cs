using Domain.Contracts.Area;

namespace Domain.Interfaces.UseCases.Area
{
    public interface IDeleteArea
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id);
    }
}