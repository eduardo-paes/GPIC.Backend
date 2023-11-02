using Application.Ports.Campus;

namespace Application.Interfaces.UseCases.Campus
{
    public interface IDeleteCampus
    {
        Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id);
    }
}