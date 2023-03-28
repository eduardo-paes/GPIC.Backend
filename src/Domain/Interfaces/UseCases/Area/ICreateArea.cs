using System.Threading.Tasks;
using Domain.Contracts.Area;

namespace Domain.Interfaces.UseCases.Area
{
    public interface ICreateArea
    {
        Task<DetailedReadAreaOutput> Execute(CreateAreaInput model);
    }
}