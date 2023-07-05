using Domain.Contracts.TypeAssistance;

namespace Domain.Interfaces.UseCases
{
    public interface IGetTypeAssistances
    {
        Task<IQueryable<ResumedReadTypeAssistanceOutput>> Execute(int skip, int take);
    }
}