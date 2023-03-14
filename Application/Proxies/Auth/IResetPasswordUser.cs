using Application.DTOs.Auth;

namespace Application.Proxies.Auth
{
    public interface IResetPasswordUser
    {
        Task<bool> Execute(UserResetPasswordDTO dto);
    }
}