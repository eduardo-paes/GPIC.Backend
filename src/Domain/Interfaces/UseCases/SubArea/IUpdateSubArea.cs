using System;
using System.Threading.Tasks;
using Domain.Contracts.SubArea;

namespace Domain.Interfaces.UseCases.SubArea
{
    public interface IUpdateSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid? id, UpdateSubAreaInput model);
    }
}