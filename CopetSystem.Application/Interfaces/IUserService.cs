using System;
using CopetSystem.Application.DTOs.User;

namespace CopetSystem.Application.Interfaces
{
	public interface IUserService
	{
        Task<IQueryable<UserReadDTO>> GetActiveUsers();
        Task<IQueryable<UserReadDTO>> GetInactiveUsers();

        Task<UserReadDTO> GetById(Guid? id);
        Task<UserReadDTO> ResetPassword(Guid? id, string? password);

        Task<UserReadDTO> Create(UserCreateDTO user);
        Task<UserReadDTO> Update(Guid? id, UserUpdateDTO user);

        Task<UserReadDTO> Activate(Guid id);
        Task<UserReadDTO> Deactivate(Guid id);
    }
}

