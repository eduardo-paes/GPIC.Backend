using System.Threading.Tasks;
using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateMainArea
    {
        Task<DetailedMainAreaOutput> Execute(CreateMainAreaInput model);
    }
}