using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interfaces.Campus
{
    public interface IUpdateCampus
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id, UpdateCampusInput input);
    }
}