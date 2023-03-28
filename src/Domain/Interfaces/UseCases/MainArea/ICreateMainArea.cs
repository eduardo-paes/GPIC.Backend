using System.Threading.Tasks;
using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases.MainArea
{
    public interface ICreateMainArea
    {
        Task<DetailedMainAreaOutput> Execute(CreateMainAreaInput model);
    }
}