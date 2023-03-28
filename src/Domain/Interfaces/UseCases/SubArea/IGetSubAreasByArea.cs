using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.SubArea;

namespace Domain.Interfaces.UseCases.SubArea
{
    public interface IGetSubAreasByArea
    {
        Task<IQueryable<ResumedReadSubAreaOutput>> Execute(Guid? areaId, int skip, int take);
    }
}