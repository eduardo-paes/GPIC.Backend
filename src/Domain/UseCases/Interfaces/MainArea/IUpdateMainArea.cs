using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interfaces.MainArea
{
    public interface IUpdateMainArea
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id, UpdateMainAreaInput input);
    }
}