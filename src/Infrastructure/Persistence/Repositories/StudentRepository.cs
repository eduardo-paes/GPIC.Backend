using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<Student> CreateAsync(Student model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Student>> GetAllAsync(int skip, int take)
        {
            return await _context.Students
            .Include(x => x.User)
            .AsAsyncEnumerable()
            .OrderBy(x => x.User?.Name)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid? id)
        {
            return await _context.Students
                .Include(x => x.User)
                .Include(x => x.Campus)
                .Include(x => x.Course)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student> DeleteAsync(Guid? id)
        {
            Student model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<Student> UpdateAsync(Student model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Student?> GetByRegistrationCodeAsync(string registrationCode)
        {
            return await _context.Students
                .Include(x => x.User)
                .Include(x => x.Campus)
                .Include(x => x.Course)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.RegistrationCode == registrationCode.ToUpper());
        }

        public async Task<Student?> GetByUserIdAsync(Guid? userId)
        {
            return await _context.Students
                .Include(x => x.User)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        #endregion Public Methods
    }
}