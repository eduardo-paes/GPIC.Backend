﻿using System;
using System.Data.Entity;
using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;
using CopetSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CopetSystem.Infra.Data.Repositories
{
	public class MainAreaRepository : IMainAreaRepository
	{
        private readonly ApplicationDbContext _context;
        public MainAreaRepository(ApplicationDbContext context) => _context = context;

        public async Task<MainArea> Create(MainArea model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<MainArea>> GetAll() => await _context.MainAreas
            .Where(x => x.DeletedAt == null).ToListAsync();

        public async Task<MainArea?> GetById(Guid? id) => await _context.MainAreas.FindAsync(id);

        public async Task<MainArea> Delete(Guid? id)
        {
            var model = await _context.MainAreas.FindAsync(id);
            if (model == null) throw new Exception("MainArea not found.");

            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<MainArea> Update(MainArea model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<MainArea> GetByCode(string? code) => await _context.MainAreas
            .FirstOrDefaultAsync(x => x.Code == code && x.DeletedAt == null);
    }
}
