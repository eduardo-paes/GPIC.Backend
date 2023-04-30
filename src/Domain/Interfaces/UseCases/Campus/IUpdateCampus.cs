using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateCampus
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id, UpdateCampusInput model);
    }
}