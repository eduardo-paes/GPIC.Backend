using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<Course> CreateAsync(Course model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Course>> GetAllAsync(int skip, int take)
        {
            return await _context.Courses
            .OrderBy(x => x.Name)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(Guid? id)
        {
            return await _context.Courses
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Course> DeleteAsync(Guid? id)
        {
            Course model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<Course> UpdateAsync(Course model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Course?> GetCourseByNameAsync(string name)
        {
            string loweredName = name.ToLower(System.Globalization.CultureInfo.CurrentCulture);
            List<Course> entities = await _context.Courses
                .Where(x => x.Name!.ToLower(System.Globalization.CultureInfo.CurrentCulture) == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion Public Methods
    }
}