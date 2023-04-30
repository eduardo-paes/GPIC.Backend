using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteMainArea
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id);
    }
}