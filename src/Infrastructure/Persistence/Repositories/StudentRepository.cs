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
        public async Task<Student> Create(Student model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Student>> GetAll(int skip, int take)
        {
            return await _context.Students
            .Include(x => x.User)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.User?.Name)
            .ToListAsync();
        }

        public async Task<Student?> GetById(Guid? id)
        {
            return await _context.Students
                .Include(x => x.User)
                .Include(x => x.Campus)
                .Include(x => x.Course)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student> Delete(Guid? id)
        {
            Student model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Student> Update(Student model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }
        #endregion Public Methods
    }
}