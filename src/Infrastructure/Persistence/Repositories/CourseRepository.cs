using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<Course> Create(Course model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Course>> GetAll(int skip, int take) => await _context.Courses
            .Where(x => x.DeletedAt == null)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<Course?> GetById(Guid? id) =>
            await _context.Courses.FindAsync(id);

        public async Task<Course> Delete(Guid? id)
        {
            var model = await this.GetById(id);
            if (model == null)
                throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Course> Update(Course model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Course?> GetCourseByName(string name)
        {
            var entities = await _context.Courses.Where(x =>
                    x.Name.ToLower() == name.ToLower()
                    && x.DeletedAt == null)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion
    }
}