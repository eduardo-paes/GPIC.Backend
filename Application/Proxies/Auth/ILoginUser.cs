using Application.DTOs.Auth;

namespace Application.Proxies.Auth
{
    public interface ILoginUser
    {
        Task<UserLoginResponseDTO> Execute(UserLoginRequestDTO dto);
    }
}