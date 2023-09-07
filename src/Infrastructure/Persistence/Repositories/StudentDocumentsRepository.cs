using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StudentDocumentsRepository : IStudentDocumentsRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentDocumentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentDocuments> CreateAsync(StudentDocuments model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<StudentDocuments> DeleteAsync(Guid? id)
        {
            StudentDocuments model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<IEnumerable<StudentDocuments>> GetAllAsync(int skip, int take)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .OrderBy(x => x.ProjectId)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<StudentDocuments?> GetByIdAsync(Guid? id)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhum Documento encontrado para o id {id}");
        }

        public async Task<StudentDocuments?> GetByProjectIdAsync(Guid? projectId)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.ProjectId == projectId)
                ?? throw new Exception($"Nenhum Documento encontrado para o projectId {projectId}");
        }

        public async Task<StudentDocuments?> GetByStudentIdAsync(Guid? studentId)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .Include(x => x.Project!.Student)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Project?.StudentId == studentId)
                ?? throw new Exception($"Nenhum Documento encontrado para o studentId {studentId}");
        }

        public async Task<StudentDocuments> UpdateAsync(StudentDocuments model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }
    }
}