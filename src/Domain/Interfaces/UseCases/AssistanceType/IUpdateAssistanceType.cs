using Domain.Contracts.AssistanceType;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> Execute(Guid? id, UpdateAssistanceTypeInput model);
    }
}