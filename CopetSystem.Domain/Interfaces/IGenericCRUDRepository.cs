using System;

namespace CopetSystem.Domain.Interfaces
{
	public interface IGenericCRUDRepository<T>
	{
        /// <summary>
        /// Busca entidade pelo Id informado.
        /// Lança uma exceção quando a entidade não é encontrada.
        /// </summary>
        /// <param name="id">Id da entidade.</param>
        /// <returns>Entidade encontrada.</returns>
        Task<T> GetById(Guid? id);

        /// <summary>
        /// Busca todas as entidades ativas.
        /// </summary>
        /// <returns>Lista de entidades ativas.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Cria entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de criação.</param>
        /// <returns>Entidade criada.</returns>
        Task<T> Create(T model);

        /// <summary>
        /// Remove entidade através do Id informado.
        /// </summary>
        /// <param name="id">Id da entidade a ser removida.</param>
        /// <returns>Entidade removida.</returns>
        Task<T> Delete(Guid? id);

        /// <summary>
        /// Atualiza entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de atualização.</param>
        /// <returns>Entidade atualizada.</returns>
        Task<T> Update(T model);
    }
}

