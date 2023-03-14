using Application.DTOs.User;

namespace Application.Proxies.User
{
    public interface IUpdateUser
    {
        Task<UserReadDTO> Execute(Guid? id, UserUpdateDTO user);
    }
}