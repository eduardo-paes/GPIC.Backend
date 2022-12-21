using System;
using CopetSystem.Application.DTOs;

namespace CopetSystem.Application.Interfaces
{
	public interface IUserService
	{
        Task<IQueryable<UserReadDTO>> GetActiveUsers();
        Task<IQueryable<UserReadDTO>> GetInactiveUsers();

        Task<UserReadDTO> GetById(int? id);
        Task<UserReadDTO> ResetPassword(long? id, string? password);

        //Task<UserReadDTO> Create(UserReadDTO user);
        //Task<UserReadDTO> Update(UserReadDTO user);
        //Task<UserReadDTO> Remove(UserReadDTO user);
    }
}

