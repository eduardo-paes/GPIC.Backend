using Domain.UseCases.Ports.AssistanceType;

namespace Domain.UseCases.Interfaces.AssistanceType
{
    public interface IGetAssistanceTypes
    {
        Task<IQueryable<ResumedReadAssistanceTypeOutput>> ExecuteAsync(int skip, int take);
    }
}