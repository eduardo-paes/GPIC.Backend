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

        public async Task<StudentDocuments> Create(StudentDocuments model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<StudentDocuments> Delete(Guid? id)
        {
            StudentDocuments model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<IEnumerable<StudentDocuments>> GetAll(int skip, int take)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<StudentDocuments?> GetById(Guid? id)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhum Documento encontrado para o id {id}");
        }

        public async Task<StudentDocuments?> GetByProjectId(Guid? projectId)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.ProjectId == projectId)
                ?? throw new Exception($"Nenhum Documento encontrado para o projectId {projectId}");
        }

        public async Task<StudentDocuments?> GetByStudentId(Guid? studentId)
        {
            return await _context.StudentDocuments
                .Include(x => x.Project)
                .Include(x => x.Project!.Student)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Project?.StudentId == studentId)
                ?? throw new Exception($"Nenhum Documento encontrado para o studentId {studentId}");
        }

        public async Task<StudentDocuments> Update(StudentDocuments model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }
    }
}