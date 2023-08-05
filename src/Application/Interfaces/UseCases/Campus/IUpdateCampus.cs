using Application.Ports.Campus;

namespace Application.Interfaces.UseCases.Campus
{
    public interface IUpdateCampus
    {
        Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id, UpdateCampusInput input);
    }
}