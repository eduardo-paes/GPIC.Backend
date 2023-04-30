using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteCampus
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id);
    }
}