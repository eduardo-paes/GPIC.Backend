using Application.DTOs.Auth;
using Application.DTOs.User;

namespace Application.Proxies.Auth
{
    public interface IRegisterUser
    {
        Task<UserReadDTO> Execute(UserRegisterDTO dto);
    }
}