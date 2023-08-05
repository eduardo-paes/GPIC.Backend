using Application.Ports.Area;

namespace Application.Interfaces.UseCases.Area
{
    public interface IGetAreaById
    {
        Task<DetailedReadAreaOutput> ExecuteAsync(Guid? id);
    }
}