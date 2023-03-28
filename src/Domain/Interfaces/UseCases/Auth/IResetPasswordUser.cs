using System.Threading.Tasks;
using Domain.Contracts.Auth;

namespace Domain.Interfaces.UseCases.Auth
{
    public interface IResetPasswordUser
    {
        Task<bool> Execute(UserResetPasswordInput dto);
    }
}