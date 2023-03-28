using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface ISubAreaRepository : IGenericCRUDRepository<SubArea>
    {
        Task<SubArea?> GetByCode(string? code);
        Task<IEnumerable<SubArea>> GetSubAreasByArea(Guid? areaId, int skip, int take);
    }
}