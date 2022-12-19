using System;
using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;
using CopetSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CopetSystem.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmail(string? email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.DeletedAt == null);
        }

        public async Task<User> GetById(int? id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetActiveUsers()
        {
            return await _context.Users
                .Where(x => x.DeletedAt == null).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetInactiveUsers()
        {
            return await _context.Users
                .Where(x => x.DeletedAt != null).ToListAsync();
        }

        public async Task<User> Remove(User user)
        {
            user.DeletedAt = DateTime.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}

