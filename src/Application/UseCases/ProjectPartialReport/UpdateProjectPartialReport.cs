using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProjectPartialReport;
using Application.Ports.ProjectPartialReport;
using Application.Validation;
using Domain.Entities.Enums;
using Domain.Interfaces.Services;

namespace Application.UseCases.ProjectPartialReport
{
    public class UpdateProjectPartialReport : IUpdateProjectPartialReport
    {
        #region Global Scope
        private readonly IProjectPartialReportRepository _projectReportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IMapper _mapper;
        public UpdateProjectPartialReport(IProjectPartialReportRepository projectReportRepository,
            IProjectRepository projectRepository,
            ITokenAuthenticationService tokenAuthenticationService,
            IMapper mapper)
        {
            _projectReportRepository = projectReportRepository;
            _projectRepository = projectRepository;
            _tokenAuthenticationService = tokenAuthenticationService;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(Guid? id, UpdateProjectPartialReportInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Obtém usuário logado
            var user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Recupera entidade que será atualizada
            Domain.Entities.ProjectPartialReport report = await _projectReportRepository.GetByIdAsync(id) ??
                throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.ProjectPartialReport));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(report.DeletedAt != null,
                "O relatório informado já foi removido.");

            // Verifica se o projeto existe
            Domain.Entities.Project project = await _projectRepository.GetByIdAsync(report.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Project));

            // Verifica se o projeto foi excluído
            UseCaseException.BusinessRuleViolation(project.DeletedAt != null,
                "O projeto informado já foi removido.");

            // Verifica se o projeto está em andamento
            UseCaseException.BusinessRuleViolation(project.Status != EProjectStatus.Started,
                "O projeto informado não está em andamento.");

            // Somente aluno ou professor do projeto pode fazer alteração no relatório
            UseCaseException.BusinessRuleViolation(user.Id != project.StudentId && user.Id != project.ProfessorId,
                "Somente o aluno ou o professor orientador do projeto pode fazer alterações no relatório.");

            // Verifica se o relatório está sendo enviado dentro do prazo
            // Relatórios podem ser entregues até 6 meses antes do prazo final
            var isBeforeDeadline = project.Notice?.PartialReportDeadline <= DateTime.UtcNow
                && project.Notice?.PartialReportDeadline.Value.AddMonths(-6) >= DateTime.UtcNow;

            // Lança exceção caso o relatório esteja sendo enviado fora do prazo
            UseCaseException.BusinessRuleViolation(isBeforeDeadline,
                "Relatório enviado fora do prazo estipulado no edital.");

            // Atualiza propriedades da entidade
            if (input.CurrentDevelopmentStage is not null)
                report.CurrentDevelopmentStage = (int)input.CurrentDevelopmentStage;
            if (input.ScholarPerformance is not null)
                report.ScholarPerformance = TryCastEnum<EScholarPerformance>(input.ScholarPerformance);
            if (input.AdditionalInfo is not null)
                report.AdditionalInfo = input.AdditionalInfo;

            // Atualiza propriedades de auditoria
            report.UserId = user.Id;

            // Salva entidade atualizada no banco
            await _projectReportRepository.UpdateAsync(report);

            // Mapeia entidade para o modelo de saída e retorna
            return _mapper.Map<DetailedReadProjectPartialReportOutput>(report);
        }

        /// <summary>
        /// Tenta converter um objeto para um tipo Enum.
        /// </summary>
        /// <param name="value">Valor a ser convertido.</param>
        /// <typeparam name="T">Tipo para o qual ser convertido.</typeparam>
        /// <returns>Objeto com tipo convertido.</returns>
        private static T TryCastEnum<T>(object? value)
        {
            try
            {
                UseCaseException.NotInformedParam(value is null, typeof(T).ToString());
                return (T)Enum.Parse(typeof(T), value?.ToString()!);
            }
            catch (Exception)
            {
                throw UseCaseException.BusinessRuleViolation($"Não foi possível converter o valor {value} para o tipo {typeof(T)}.");
            }
        }
    }
}