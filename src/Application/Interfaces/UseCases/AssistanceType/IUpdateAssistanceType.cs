using Application.Ports.AssistanceType;

namespace Application.Interfaces.UseCases.AssistanceType
{
    public interface IUpdateAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id, UpdateAssistanceTypeInput input);
    }
}