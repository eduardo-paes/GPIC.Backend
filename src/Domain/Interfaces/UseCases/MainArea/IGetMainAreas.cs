using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases
{
    public interface IGetMainAreas
    {
        Task<IQueryable<ResumedReadMainAreaOutput>> Execute(int skip, int take);
    }
}