using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;
using CopetSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CopetSystem.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;
        
        #region CRUD Methods
        public async Task<User> GetById(Guid? id) => await _context.Users
            .FindAsync(id)
                ?? throw new Exception("User not found.");

        public async Task<IEnumerable<User>> GetActiveUsers(int skip, int take) => await _context.Users
            .Where(x => x.DeletedAt == null).ToListAsync();

        public async Task<IEnumerable<User>> GetInactiveUsers(int skip, int take) => await _context.Users
            .Where(x => x.DeletedAt != null).ToListAsync();

        public async Task<User> Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        #endregion

        #region Auth Methods
        public async Task<User> Register(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByEmail(string? email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);                    
        }

        public async Task<User?> GetUserByCPF(string? cpf)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == cpf);
        }
        #endregion
    }
}