using Domain.UseCases.Ports.AssistanceType;

namespace Domain.UseCases.Interfaces.AssistanceType
{
    public interface ICreateAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> Execute(CreateAssistanceTypeInput model);
    }
}