using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interfaces.Campus
{
    public interface IDeleteCampus
    {
        Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id);
    }
}