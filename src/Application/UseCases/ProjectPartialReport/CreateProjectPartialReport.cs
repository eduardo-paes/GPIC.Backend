using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.ProjectPartialReport;
using Application.Interfaces.UseCases.ProjectPartialReport;
using Application.Validation;
using Domain.Entities.Enums;
using Domain.Interfaces.Services;

namespace Application.UseCases.ProjectPartialReport
{
    public class CreateProjectPartialReport : ICreateProjectPartialReport
    {
        #region Global Scope
        private readonly IProjectPartialReportRepository _projectReportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IMapper _mapper;
        public CreateProjectPartialReport(IProjectPartialReportRepository projectReportRepository,
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

        public async Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(CreateProjectPartialReportInput input)
        {
            // Obtém usuário logado
            var userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Obtém id do usuário e id de acordo com perfil logado
            var userClaim = userClaims!.Values.FirstOrDefault();
            var actorId = userClaims.Keys.FirstOrDefault();

            // Cria entidade a partir do modelo
            Domain.Entities.ProjectPartialReport report = new(
                input.ProjectId,
                input.CurrentDevelopmentStage,
                EnumExtensions.TryCastEnum<EScholarPerformance>(input.ScholarPerformance),
                input.AdditionalInfo,
                userClaim!.Id
            );

            // Verifica se o projeto existe
            Domain.Entities.Project project = await _projectRepository.GetByIdAsync(report.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Project));

            // Verifica se o projeto foi excluído
            UseCaseException.BusinessRuleViolation(project.DeletedAt != null,
                "O projeto informado já foi removido.");

            // Verifica se o projeto está em andamento
            UseCaseException.BusinessRuleViolation(project.Status != EProjectStatus.Started,
                "O projeto informado não está em andamento.");

            // Somente aluno ou professor do projeto pode fazer inclusão de relatório
            UseCaseException.BusinessRuleViolation(actorId != project.StudentId && actorId != project.ProfessorId,
                "Somente o aluno ou o professor orientador do projeto pode fazer inclusão de relatório.");

            // Verifica se o relatório está sendo enviado dentro do prazo
            // Relatórios podem ser entregues até 6 meses antes do prazo final
            var deadline = project.Notice?.PartialReportDeadline ?? throw UseCaseException.BusinessRuleViolation("O prazo para envio de relatório parcial não foi definido.");
            var isBeforeDeadline = deadline < DateTime.UtcNow || deadline.AddMonths(-6) > DateTime.UtcNow;

            // Lança exceção caso o relatório esteja sendo enviado fora do prazo
            UseCaseException.BusinessRuleViolation(isBeforeDeadline,
                "Relatório enviado fora do prazo estipulado no edital.");

            // Cria entidade
            report = await _projectReportRepository.CreateAsync(report);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadProjectPartialReportOutput>(report);
        }
    }
}