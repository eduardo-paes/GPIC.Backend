using Domain.UseCases.Ports.AssistanceType;

namespace Domain.UseCases.Interfaces.AssistanceType
{
    public interface IUpdateAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> Execute(Guid? id, UpdateAssistanceTypeInput input);
    }
}