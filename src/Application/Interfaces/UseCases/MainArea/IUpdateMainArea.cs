using Application.Ports.MainArea;

namespace Application.Interfaces.UseCases.MainArea
{
    public interface IUpdateMainArea
    {
        Task<DetailedMainAreaOutput> ExecuteAsync(Guid? id, UpdateMainAreaInput input);
    }
}