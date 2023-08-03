using Domain.UseCases.Ports.AssistanceType;

namespace Domain.UseCases.Interfaces.AssistanceType
{
    public interface IGetAssistanceTypeById
    {
        Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id);
    }
}