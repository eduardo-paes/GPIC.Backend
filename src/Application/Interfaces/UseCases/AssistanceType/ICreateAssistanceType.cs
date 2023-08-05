using Application.Ports.AssistanceType;

namespace Application.Interfaces.UseCases.AssistanceType
{
    public interface ICreateAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(CreateAssistanceTypeInput model);
    }
}