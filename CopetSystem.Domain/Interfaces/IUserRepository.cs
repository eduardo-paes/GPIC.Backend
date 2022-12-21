using System;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
	public interface IUserRepository
	{
        Task<IEnumerable<User>> GetActiveUsers();
        Task<IEnumerable<User>> GetInactiveUsers();

        Task<User> GetById(long? id);
        Task<User> Login(string? email, string? password);
        Task<User> ResetPassword(long? id, string? password);

        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> Remove(User user);
    }
}

