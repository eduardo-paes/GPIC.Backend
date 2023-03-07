using CopetSystem.Application.DTOs.User;

namespace CopetSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<IQueryable<UserReadDTO>> GetActiveUsers(int skip, int take);
        Task<IQueryable<UserReadDTO>> GetInactiveUsers(int skip, int take);

        Task<UserReadDTO> GetById(Guid? id);
        Task<UserReadDTO> Update(Guid? id, UserUpdateDTO user);

        Task<UserReadDTO> Activate(Guid id);
        Task<UserReadDTO> Deactivate(Guid id);
    }
}

