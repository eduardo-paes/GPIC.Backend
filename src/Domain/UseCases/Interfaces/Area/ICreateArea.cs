using Domain.UseCases.Ports.Area;

namespace Domain.UseCases.Interfaces.Area
{
    public interface ICreateArea
    {
        Task<DetailedReadAreaOutput> ExecuteAsync(CreateAreaInput model);
    }
}