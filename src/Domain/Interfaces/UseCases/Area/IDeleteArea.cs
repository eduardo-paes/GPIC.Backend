using Domain.Contracts.Area;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteArea
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id);
    }
}