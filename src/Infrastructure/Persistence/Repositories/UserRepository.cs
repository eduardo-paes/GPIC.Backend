using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region CRUD Methods
        public async Task<User?> GetById(Guid? id) => await _context.Users
            .FindAsync(id);

        public async Task<IEnumerable<User>> GetActiveUsers(int skip, int take) => await _context.Users
            .Skip(skip)
            .Take(take)
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<IEnumerable<User>> GetInactiveUsers(int skip, int take) => await _context.Users
            .IgnoreQueryFilters()
            .AsAsyncEnumerable()
            .Where(x => x.DeletedAt != null)
            .Skip(skip)
            .Take(take)
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<User> Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Create(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }
        #endregion

        #region Auth Methods
        public async Task<User?> GetUserByEmail(string? email) => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetUserByCPF(string? cpf) => await _context.Users.FirstOrDefaultAsync(x => x.CPF == cpf);
        #endregion
    }
}