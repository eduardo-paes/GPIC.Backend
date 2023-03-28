using System.Threading.Tasks;
using Domain.Contracts.Auth;
using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases.Auth
{
    public interface IRegisterUser
    {
        Task<UserReadOutput> Execute(UserRegisterInput dto);
    }
}