using Domain.UseCases.Ports.AssistanceType;

namespace Domain.UseCases.Interfaces.AssistanceType
{
    public interface IGetAssistanceTypes
    {
        Task<IQueryable<ResumedReadAssistanceTypeOutput>> Execute(int skip, int take);
    }
}