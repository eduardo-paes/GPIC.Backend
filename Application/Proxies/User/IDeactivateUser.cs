using Application.DTOs.User;

namespace Application.Proxies.User
{
    public interface IDeactivateUser
    {
        Task<UserReadDTO> Execute(Guid id);
    }
}