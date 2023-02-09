using System;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Domain.Interfaces
{
	public interface IUserRepository
	{
        Task<IEnumerable<User>> GetActiveUsers();
        Task<IEnumerable<User>> GetInactiveUsers();

        Task<User> GetById(Guid? id);
        Task<User> Update(User user);

        #region Auth
        Task<User> Register(User user);
        Task<User?> GetUserByEmail(string? email);
        #endregion
    }
}

