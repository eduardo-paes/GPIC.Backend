using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.Area;

namespace Domain.Interfaces.UseCases.Area
{
    public interface IGetAreasByMainArea
    {
        Task<IQueryable<ResumedReadAreaOutput>> Execute(Guid? mainAreaId, int skip, int take);
    }
}