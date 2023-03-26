using Domain.Contracts.Area;

namespace Domain.Interfaces.Area
{
    public interface ICreateArea
    {
        Task<DetailedReadAreaOutput> Execute(CreateAreaInput model);
    }
}