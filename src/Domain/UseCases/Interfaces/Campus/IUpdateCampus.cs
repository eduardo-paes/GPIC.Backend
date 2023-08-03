using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interfaces.Campus
{
    public interface IUpdateCampus
    {
        Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id, UpdateCampusInput input);
    }
}