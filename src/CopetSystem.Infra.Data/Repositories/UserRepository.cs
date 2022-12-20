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

        public async Task<User> GetByEmail(string? email) => await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email && x.DeletedAt == null)
            ?? throw new Exception("User not found.");

        public async Task<User> GetById(int? id) => await _context.Users
            .FindAsync(id)
            ?? throw new Exception("User not found.");

        public async Task<IEnumerable<User>> GetActiveUsers() => await _context.Users
            .Where(x => x.DeletedAt == null).ToListAsync();

        public async Task<IEnumerable<User>> GetInactiveUsers() => await _context.Users
            .Where(x => x.DeletedAt != null).ToListAsync();

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

