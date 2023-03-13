using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAreaRepository
    {
        /// <summary>
        /// Busca entidade pelo Id informado.
        /// Lança uma exceção quando a entidade não é encontrada.
        /// </summary>
        /// <param name="id">Id da entidade.</param>
        /// <returns>Entidade encontrada.</returns>
        Task<Area> GetById(Guid? id);

        /// <summary>
        /// Cria entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de criação.</param>
        /// <returns>Entidade criada.</returns>
        Task<Area> Create(Area model);

        /// <summary>
        /// Remove entidade através do Id informado.
        /// </summary>
        /// <param name="id">Id da entidade a ser removida.</param>
        /// <returns>Entidade removida.</returns>
        Task<Area> Delete(Guid? id);

        /// <summary>
        /// Atualiza entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de atualização.</param>
        /// <returns>Entidade atualizada.</returns>
        Task<Area> Update(Area model);

        Task<Area?> GetByCode(string? code);
        Task<IEnumerable<Area>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);
    }
}