using System;
namespace Application.Interfaces.Primitives
{
    public interface ICrudBaseService
    {
        Task<T> GetById<T>(Guid? id);
        Task<IQueryable<T>> GetAll<T>(int? skip, int? take);

        Task<T> Create<T>(T model);
        Task<T> Update<T>(Guid? id, T model);
        Task<T> Delete<T>(Guid id);
    }
}

