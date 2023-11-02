using Application.Ports.Campus;

namespace Application.Interfaces.UseCases.Campus
{
    public interface IGetCampusById
    {
        Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id);
    }
}