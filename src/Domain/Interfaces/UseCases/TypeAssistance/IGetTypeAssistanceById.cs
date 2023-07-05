using Domain.Contracts.TypeAssistance;

namespace Domain.Interfaces.UseCases
{
    public interface IGetTypeAssistanceById
    {
        Task<DetailedReadTypeAssistanceOutput> Execute(Guid? id);
    }
}