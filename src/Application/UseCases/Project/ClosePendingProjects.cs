using Application.Interfaces.UseCases.Project;
using Domain.Interfaces.Repositories;

namespace Application.UseCases.Project
{
    public class ClosePendingProjects : IClosePendingProjects
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        public ClosePendingProjects(
            IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        #endregion

        public async Task<string> ExecuteAsync()
        {
            /// Realiza o cancelamento de todos os projetos pendentes e com o prazo de resolução vendido.
            /// Casos:
            ///  - Projetos abertos e com o prazo de submissão vencido;
            //   - Projetos rejeitados e com o prazo de recurso vencido;
            //   - Projetos aprovados e com entrega de documentação do aluno vencida;
            //   - Projetos pendentes de documentação e com o prazo de entrega vencido.
            return await _projectRepository.ClosePendingAndOverdueProjectsAsync();
        }
    }
}