using System;

namespace CopetSystem.Domain.Interfaces
{
	public interface IGenericCRUDRepository<T>
	{
        Task<T> GetById(Guid? id);
        Task<IEnumerable<T>> GetAll();

        Task<T> Create(T model);
        Task<T> Delete(Guid? id);
        Task<T> Update(T model);
    }
}

