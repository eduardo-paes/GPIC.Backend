using Domain.Contracts.MainArea;

namespace Domain.Interfaces.MainArea
{
    public interface ICreateMainArea
    {
        Task<DetailedMainAreaOutput> Execute(CreateMainAreaInput model);
    }
}