using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interfaces.MainArea
{
    public interface ICreateMainArea
    {
        Task<DetailedMainAreaOutput> Execute(CreateMainAreaInput model);
    }
}