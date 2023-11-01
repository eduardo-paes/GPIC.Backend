using Application.Interfaces.UseCases.Project;
using Domain.Entities.Enums;
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
            var projects = await _projectRepository.GetPendingAndOverdueProjectsAsync();

            // Verifica se existem projetos pendentes e com prazo vencido
            if (projects.Count == 0)
                return "Nenhum projeto pendente e com prazo vencido foi encontrado.";

            // Atualiza status dos projetos
            foreach (var project in projects)
            {
                project.Status = EProjectStatus.Canceled;
                project.StatusDescription = "Projeto cancelado automaticamente por falta de ação dentro do prazo estipulado.";
            }

            // Atualiza projetos no banco de dados
            var cancelledProjects = await _projectRepository.UpdateManyAsync(projects);
            if (cancelledProjects != projects.Count)
                return $"Ocorreu um erro ao cancelar os projetos pendentes e com prazo vencido. Foram cancelados {cancelledProjects} de {projects.Count} projetos.";

            // Retorna mensagem de sucesso
            return $"{projects.Count} projetos pendentes e com prazo de resolução vencido foram cancelados com sucesso.";
        }
    }
}