using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases.Campus
{
    public interface IGetCampusById
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id);
    }
}