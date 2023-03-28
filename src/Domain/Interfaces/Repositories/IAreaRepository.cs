using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IAreaRepository : IGenericCRUDRepository<Area>
    {
        Task<Area?> GetByCode(string? code);
        Task<IEnumerable<Area>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);
    }
}