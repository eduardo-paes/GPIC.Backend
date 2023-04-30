using System;
using System.Threading.Tasks;
using Domain.Contracts.Area;

namespace Domain.Interfaces.UseCases
{
    public interface IGetAreaById
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id);
    }
}