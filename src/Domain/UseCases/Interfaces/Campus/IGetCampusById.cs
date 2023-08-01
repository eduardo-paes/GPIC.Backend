using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interfaces.Campus
{
    public interface IGetCampusById
    {
        Task<DetailedReadCampusOutput> Execute(Guid? id);
    }
}