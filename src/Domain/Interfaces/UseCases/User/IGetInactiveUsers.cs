using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases
{
    public interface IGetInactiveUsers
    {
        Task<IQueryable<UserReadOutput>> Execute(int skip, int take);
    }
}