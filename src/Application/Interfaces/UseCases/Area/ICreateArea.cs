using Application.Ports.Area;

namespace Application.Interfaces.UseCases.Area
{
    public interface ICreateArea
    {
        Task<DetailedReadAreaOutput> ExecuteAsync(CreateAreaInput model);
    }
}