using Domain.Contracts.MainArea;

namespace Domain.Interfaces.MainArea
{
    public interface IUpdateMainArea
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id, UpdateMainAreaInput model);
    }
}