using System;
using System.Threading.Tasks;
using Domain.Contracts.MainArea;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateMainArea
    {
        Task<DetailedMainAreaOutput> Execute(Guid? id, UpdateMainAreaInput model);
    }
}