using Application.DTOs.User;

namespace Application.Proxies.User
{
    public interface IActivateUser
    {
        Task<UserReadDTO> Execute(Guid id);
    }
}