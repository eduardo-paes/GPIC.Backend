using Domain.Contracts.AssistanceType;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> Execute(CreateAssistanceTypeInput model);
    }
}