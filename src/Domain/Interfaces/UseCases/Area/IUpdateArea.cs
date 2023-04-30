using System;
using System.Threading.Tasks;
using Domain.Contracts.Area;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateArea
    {
        Task<DetailedReadAreaOutput> Execute(Guid? id, UpdateAreaInput model);
    }
}