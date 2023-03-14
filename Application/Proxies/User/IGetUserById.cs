using Application.DTOs.User;

namespace Application.Proxies.User
{
    public interface IGetUserById
    {
        Task<UserReadDTO> Execute(Guid? id);
    }
}