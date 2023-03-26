using Domain.Contracts.MainArea;

namespace Domain.Interfaces.MainArea
{
    public interface IDeleteMainArea
    {
        Task<DetailedMainAreaOutput> Execute(Guid id);
    }
}