using Domain.UseCases.Ports.Area;

namespace Domain.UseCases.Interfaces.Area
{
    public interface ICreateArea
    {
        Task<DetailedReadAreaOutput> Execute(CreateAreaInput model);
    }
}