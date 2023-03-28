using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases.MainArea
{
    public interface IDeleteMainArea
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id);
    }
}