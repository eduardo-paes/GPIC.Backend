using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases.Campus
{
    public interface IDeleteCampus
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id);
    }
}