using Domain.Contracts.TypeAssistance;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateTypeAssistance
    {
        Task<DetailedReadTypeAssistanceOutput> Execute(CreateTypeAssistanceInput model);
    }
}