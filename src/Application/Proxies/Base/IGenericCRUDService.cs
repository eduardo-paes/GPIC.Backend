namespace Application.Proxies.Base
{
    public interface IGenericCRUDService
    {
        /// <summary>
        /// Busca entidade pelo Id informado.
        /// Lança uma exceção quando a entidade não é encontrada.
        /// </summary>
        /// <param name="id">Id da entidade.</param>
        /// <returns>Entidade encontrada.</returns>
        Task<ResponseDTO> GetById(Guid? id);

        /// <summary>
        /// Busca todas as entidades ativas.
        /// </summary>
        /// <returns>Lista de entidades ativas.</returns>
        Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take);

        /// <summary>
        /// Cria entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de criação.</param>
        /// <returns>Entidade criada.</returns>
        Task<ResponseDTO> Create(RequestDTO model);

        /// <summary>
        /// Remove entidade através do Id informado.
        /// </summary>
        /// <param name="id">Id da entidade a ser removida.</param>
        /// <returns>Entidade removida.</returns>
        Task<ResponseDTO> Delete(Guid? id);

        /// <summary>
        /// Atualiza entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de atualização.</param>
        /// <returns>Entidade atualizada.</returns>
        Task<ResponseDTO> Update(RequestDTO model);
    }
}