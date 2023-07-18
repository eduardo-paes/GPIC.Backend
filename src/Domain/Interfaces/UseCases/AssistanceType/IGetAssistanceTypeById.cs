using Domain.Contracts.AssistanceType;

namespace Domain.Interfaces.UseCases
{
    public interface IGetAssistanceTypeById
    {
        Task<DetailedReadAssistanceTypeOutput> Execute(Guid? id);
    }
}