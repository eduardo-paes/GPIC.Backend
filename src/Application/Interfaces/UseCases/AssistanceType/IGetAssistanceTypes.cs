using Application.Ports.AssistanceType;

namespace Application.Interfaces.UseCases.AssistanceType
{
    public interface IGetAssistanceTypes
    {
        Task<IQueryable<ResumedReadAssistanceTypeOutput>> ExecuteAsync(int skip, int take);
    }
}