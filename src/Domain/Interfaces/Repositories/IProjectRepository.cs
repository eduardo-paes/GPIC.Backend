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
        /// Permite a busca de todos os projetos (abertos ou fechados).
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <param name="isClosed">Filtra por projetos encerrados.</param>
        /// <returns>Retorna todos os projetos.</returns>
        Task<IEnumerable<Project>> GetProjectsAsync(int skip, int take, bool isClosed = false);

        /// <summary>
        /// Permite a busca dos projetos (abertos ou fechados) associados ao aluno.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <param name="id">Id do aluno.</param>
        /// <param name="isClosed">Filtra por projetos encerrados.</param>
        /// <returns>Retorna os projetos do aluno.</returns>
        Task<IEnumerable<Project>> GetStudentProjectsAsync(int skip, int take, Guid? id, bool isClosed = false);

        /// <summary>
        /// Permite a busca dos projetos (abertos ou fechados) associados ao professor.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <param name="id">Id do professor.</param>
        /// <param name="isClosed">Filtra por projetos encerrados.</param>
        /// <returns>Retorna os projetos do professor.</returns>
        Task<IEnumerable<Project>> GetProfessorProjectsAsync(int skip, int take, Guid? id, bool isClosed = false);

        /// <summary>
        /// Permite a busca dos projetos em avaliação e que não estão associados ao professor.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <param name="professorId">Id do professor.</param>
        /// <returns>Retorna os projetos em avaliação.</returns>
        Task<IEnumerable<Project>> GetProjectsToEvaluateAsync(int skip, int take, Guid? professorId);

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

        /// <summary>
        /// Obtém projeto pelo Id do Edital informado.
        /// </summary>
        /// <param name="noticeId">Id do Edital.</param>
        /// <returns>Projetos encontrados.</returns>
        Task<IEnumerable<Project>> GetProjectByNoticeAsync(Guid? noticeId);

        /// <summary>
        /// Obtém projetos que possuem data de entrega de relatório parcial ou final próxima.
        /// </summary>
        /// <returns>Projetos encontrados.</returns>
        /// <remarks>
        /// A data de entrega de relatório parcial ou final é considerada próxima quando a mesma está a um mês ou 7 dias de distância.
        /// </remarks>
        Task<IEnumerable<Project>> GetProjectsWithCloseReportDueDateAsync();

        /// <summary>
        /// Obtém projetos com alguma pendência e cujo prazo de resolução da pendência esteja vencido.
        /// </summary>
        /// <returns>Resultado do processo de encerramento dos projetos.</returns>
        Task<string> ClosePendingAndOverdueProjectsAsync();
    }
}