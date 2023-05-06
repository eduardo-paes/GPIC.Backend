using Adapters.DTOs.User;

namespace Adapters.Proxies
{
    public interface IUserService
    {
        Task<UserReadDTO> ActivateUser(Guid? id);
        Task<UserReadDTO> DeactivateUser(Guid? id);
        Task<IEnumerable<UserReadDTO>> GetActiveUsers(int skip, int take);
        Task<IEnumerable<UserReadDTO>> GetInactiveUsers(int skip, int take);
        Task<UserReadDTO> GetUserById(Guid? id);
        Task<UserReadDTO> UpdateUser(Guid? id, UserUpdateDTO dto);
    }
}