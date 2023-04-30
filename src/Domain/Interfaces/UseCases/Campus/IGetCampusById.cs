using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases
{
    public interface IGetCampusById
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id);
    }
}