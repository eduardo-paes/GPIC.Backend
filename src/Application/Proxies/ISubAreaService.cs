namespace Application.Proxies.SubArea
{
    public public interface ISubAreaService
    {
        /// <summary>
        /// Busca entidade pelo Id informado.
        /// Lança uma exceção quando a entidade não é encontrada.
        /// </summary>
        /// <param name="id">Id da entidade.</param>
        /// <returns>Entidade encontrada.</returns>
        Task<DetailedReadSubAreaDTO> GetById(Guid? id);

        /// <summary>
        /// Busca todas as entidades ativas.
        /// </summary>
        /// <returns>Lista de entidades ativas.</returns>
        Task<IEnumerable<ResumedReadSubAreaDTO>> GetAll(int skip, int take);

        /// <summary>
        /// Cria entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de criação.</param>
        /// <returns>Entidade criada.</returns>
        Task<ResumedReadSubAreaDTO> Create(CreateSubAreaDTO model);

        /// <summary>
        /// Remove entidade através do Id informado.
        /// </summary>
        /// <param name="id">Id da entidade a ser removida.</param>
        /// <returns>Entidade removida.</returns>
        Task<ResumedReadSubAreaDTO> Delete(Guid? id);

        /// <summary>
        /// Atualiza entidade conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de atualização.</param>
        /// <returns>Entidade atualizada.</returns>
        Task<ResumedReadSubAreaDTO> Update(UpdateSubAreaDTO model);
    }
}