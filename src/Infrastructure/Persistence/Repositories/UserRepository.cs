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
        public async Task<User?> GetByIdAsync(Guid? id)
        {
            return await _context.Users
            .FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync(int skip, int take)
        {
            return await _context.Users
            .Skip(skip)
            .Take(take)
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetInactiveUsersAsync(int skip, int take)
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

        public async Task<User> UpdateAsync(User user)
        {
            _ = _context.Update(user);
            _ = await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> CreateAsync(User user)
        {
            _ = _context.Add(user);
            _ = await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteAsync(Guid? id)
        {
            User model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }
        #endregion CRUD Methods

        #region Auth Methods
        public async Task<User?> GetUserByEmailAsync(string? email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserByCPFAsync(string? cpf)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        public async Task<User?> GetCoordinatorAsync()
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.IsCoordinator);
        }
        #endregion Auth Methods
    }
}