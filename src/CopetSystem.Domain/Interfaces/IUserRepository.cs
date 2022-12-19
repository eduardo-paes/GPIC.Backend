using System;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
	public interface IUserRepository
	{
        Task<IEnumerable<User>> GetActiveUsers();
        Task<IEnumerable<User>> GetInactiveUsers();

        Task<User> GetById(int? id);
        Task<User> GetByEmail(string? email);

        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> Remove(User user);
    }
}

