using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interfaces.MainArea
{
    public interface ICreateMainArea
    {
        Task<DetailedMainAreaOutput> ExecuteAsync(CreateMainAreaInput model);
    }
}