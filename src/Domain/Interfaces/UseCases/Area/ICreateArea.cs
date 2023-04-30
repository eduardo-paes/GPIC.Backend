using Domain.Contracts.Area;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateArea
    {
        Task<DetailedReadAreaOutput> Execute(CreateAreaInput model);
    }
}