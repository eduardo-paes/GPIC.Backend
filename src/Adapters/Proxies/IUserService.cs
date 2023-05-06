using Adapters.DTOs.User;

namespace Adapters.Proxies
{
    public interface IUserService
    {
        Task<UserReadDTO> ActivateUser(Guid? id);
        Task<UserReadDTO> DeactivateUser(Guid? id);
        Task<IQueryable<UserReadDTO>> GetActiveUsers(int skip, int take);
        Task<IQueryable<UserReadDTO>> GetInactiveUsers(int skip, int take);
        Task<UserReadDTO> GetUserById(Guid? id);
        Task<UserReadDTO> UpdateUser(Guid? id, UserUpdateDTO dto);
    }
}