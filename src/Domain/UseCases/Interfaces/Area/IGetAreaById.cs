using Domain.UseCases.Ports.Area;

namespace Domain.UseCases.Interfaces.Area
{
    public interface IGetAreaById
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id);
    }
}