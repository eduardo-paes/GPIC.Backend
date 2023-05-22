﻿using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProgramTypeRepository : IProgramTypeRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public ProgramTypeRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<ProgramType> Create(ProgramType model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<ProgramType>> GetAll(int skip, int take) => await _context.ProgramTypes
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<ProgramType?> GetById(Guid? id) =>
            await _context.ProgramTypes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<ProgramType> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<ProgramType> Update(ProgramType model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProgramType?> GetProgramTypeByName(string name)
        {
            string loweredName = name.ToLower();
            var entities = await _context.ProgramTypes
                .Where(x => x.Name!.ToLower() == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion
    }
}