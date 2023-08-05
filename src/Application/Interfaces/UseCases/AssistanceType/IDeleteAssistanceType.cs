using Application.Ports.AssistanceType;

namespace Application.Interfaces.UseCases.AssistanceType
{
    public interface IDeleteAssistanceType
    {
        Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id);
    }
}