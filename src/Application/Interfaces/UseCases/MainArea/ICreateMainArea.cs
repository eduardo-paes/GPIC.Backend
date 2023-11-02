using Application.Ports.MainArea;

namespace Application.Interfaces.UseCases.MainArea
{
    public interface ICreateMainArea
    {
        Task<DetailedMainAreaOutput> ExecuteAsync(CreateMainAreaInput model);
    }
}