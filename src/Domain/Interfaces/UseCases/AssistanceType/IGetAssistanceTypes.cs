using Domain.Contracts.AssistanceType;

namespace Domain.Interfaces.UseCases
{
    public interface IGetAssistanceTypes
    {
        Task<IQueryable<ResumedReadAssistanceTypeOutput>> Execute(int skip, int take);
    }
}