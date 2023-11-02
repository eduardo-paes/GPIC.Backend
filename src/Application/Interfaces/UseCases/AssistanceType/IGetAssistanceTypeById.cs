using Application.Ports.AssistanceType;

namespace Application.Interfaces.UseCases.AssistanceType
{
    public interface IGetAssistanceTypeById
    {
        Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id);
    }
}