using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases.Campus
{
    public interface IUpdateCampus
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id, UpdateCampusInput model);
    }
}