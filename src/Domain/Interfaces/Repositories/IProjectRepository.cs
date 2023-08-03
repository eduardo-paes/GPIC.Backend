using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Obtém projeto pelo Id informado.
        /// </summary>
        /// <param name="id">Id do projeto.</param>
        /// <returns>Projeto encontrado.</returns>
        Task<Project?> GetByIdAsync(Guid? id);

        /// <summary>
        /// Permite a busca de todos os projetos abertos.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="isClosed">Filtra por projetos encerrados.</param>
        /// <returns>Retorna todos os projetos.</returns>
        Task<IEnumerable<Project>> GetProjectsAsync(int skip, int take, bool isClosed = false);

        /// <summary>
        /// Permite a busca dos projetos associados ao aluno.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="id">Id do aluno.</param>
        /// <param name="isClosed">Filtra por projetos encerrados.</param>
        /// <returns>Retorna os projetos do aluno.</returns>
        Task<IEnumerable<Project>> GetStudentProjectsAsync(int skip, int take, Guid? id, bool isClosed = false);

        /// <summary>
        /// Permite a busca dos projetos associados ao professor.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="id">Id do professor.</param>
        /// <param name="isClosed">Filtra por projetos encerrados.</param>
        /// <returns>Retorna os projetos do professor.</returns>
        Task<IEnumerable<Project>> GetProfessorProjectsAsync(int skip, int take, Guid? id, bool isClosed = false);

        /// <summary>
        /// Cria projeto conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de criação.</param>
        /// <returns>Projeto criado.</returns>
        Task<Project> CreateAsync(Project model);

        /// <summary>
        /// Remove projeto através do Id informado.
        /// </summary>
        /// <param name="id">Id do projeto a ser removido.</param>
        /// <returns>Projeto removido.</returns>
        Task<Project> DeleteAsync(Guid? id);

        /// <summary>
        /// Atualiza projeto conforme parâmetros fornecidos.
        /// </summary>
        /// <param name="model">Parâmetros de atualização.</param>
        /// <returns>Projeto atualizado.</returns>
        Task<Project> UpdateAsync(Project model);
    }
}