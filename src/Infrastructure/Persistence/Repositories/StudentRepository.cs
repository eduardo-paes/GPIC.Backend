using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<Student> Create(Student model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Student>> GetAll(int skip, int take) => await _context.Students
            .Where(x => x.DeletedAt == null)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .ToListAsync();

        public async Task<Student?> GetById(Guid? id) =>
            await _context.Students.FindAsync(id);

        public async Task<Student> Delete(Guid? id)
        {
            var model = await this.GetById(id);
            if (model == null)
                throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Student> Update(Student model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion
    }
}