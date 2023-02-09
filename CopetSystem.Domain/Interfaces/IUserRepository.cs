using System;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
	public interface IUserRepository
	{
        Task<IEnumerable<User>> GetActiveUsers();
        Task<IEnumerable<User>> GetInactiveUsers();

        Task<User> GetById(Guid? id);
        Task<User> Login(string? email, string? password);
        Task<User> ResetPassword(Guid? id, string? password);

        Task<User> Create(User user);
        Task<User> Update(User user);
    }
}

