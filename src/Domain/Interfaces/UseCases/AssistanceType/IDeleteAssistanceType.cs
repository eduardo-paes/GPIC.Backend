using Domain.Contracts.AssistanceType;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> Execute(Guid? id);
    }
}