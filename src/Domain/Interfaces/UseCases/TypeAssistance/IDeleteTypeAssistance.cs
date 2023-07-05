using Domain.Contracts.TypeAssistance;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteTypeAssistance
    {
        Task<DetailedReadTypeAssistanceOutput> Execute(Guid? id);
    }
}