using System;

namespace CopetSystem.Domain.Interfaces
{
	public interface IGenericCRUDRepository<T>
	{
        Task<T> Get();
        Task<IEnumerable<T>> GetAll();

        Task<T> Create(T model);
        Task<T> Remove(T model);
        Task<T> Update(T model);
    }
}

