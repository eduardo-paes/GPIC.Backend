using Domain.UseCases.Ports.AssistanceType;

namespace Domain.UseCases.Interfaces.AssistanceType
{
    public interface IUpdateAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id, UpdateAssistanceTypeInput input);
    }
}