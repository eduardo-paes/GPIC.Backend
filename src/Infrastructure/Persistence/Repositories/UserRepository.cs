using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region CRUD Methods
        public async Task<User?> GetById(Guid? id)
        {
            return await _context.Users
            .FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetActiveUsers(int skip, int take)
        {
            return await _context.Users
            .Skip(skip)
            .Take(take)
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetInactiveUsers(int skip, int take)
        {
            return await _context.Users
            .IgnoreQueryFilters()
            .AsAsyncEnumerable()
            .Where(x => x.DeletedAt != null)
            .Skip(skip)
            .Take(take)
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<User> Update(User user)
        {
            _ = _context.Update(user);
            _ = await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Create(User user)
        {
            _ = _context.Add(user);
            _ = await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(Guid? id)
        {
            User model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }
        #endregion CRUD Methods

        #region Auth Methods
        public async Task<User?> GetUserByEmail(string? email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserByCPF(string? cpf)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.CPF == cpf);
        }
        #endregion Auth Methods
    }
}