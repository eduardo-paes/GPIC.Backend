using Domain.Contracts.TypeAssistance;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateTypeAssistance
    {
        Task<DetailedReadTypeAssistanceOutput> Execute(Guid? id, UpdateTypeAssistanceInput model);
    }
}