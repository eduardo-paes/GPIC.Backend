using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Ports.ProjectFinalReport;
using Application.Validation;

namespace Application.UseCases.ProjectFinalReport
{
    public class UpdateProjectFinalReport : IUpdateProjectFinalReport
    {
        #region Global Scope
        private readonly IProjectFinalReportRepository _projectReportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStorageFileService _storageFileService;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IMapper _mapper;
        public UpdateProjectFinalReport(IProjectFinalReportRepository projectReportRepository,
            IProjectRepository projectRepository,
            IStorageFileService storageFileService,
            ITokenAuthenticationService tokenAuthenticationService,
            IMapper mapper)
        {
            _projectReportRepository = projectReportRepository;
            _projectRepository = projectRepository;
            _storageFileService = storageFileService;
            _tokenAuthenticationService = tokenAuthenticationService;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(Guid? id, UpdateProjectFinalReportInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se o arquivo foi informado
            UseCaseException.NotInformedParam(input.ReportFile is null, nameof(input.ReportFile));

            // Obtém usuário logado
            var user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // TODO: Verifica se usuário logado pode realizar a operação

            // Recupera entidade que será atualizada
            Domain.Entities.ProjectFinalReport report = await _projectReportRepository.GetByIdAsync(id) ??
                throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.ProjectFinalReport));

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
            UseCaseException.BusinessRuleViolation(project.Status != Domain.Entities.Enums.EProjectStatus.Started,
                "O projeto informado não está em andamento.");

            // Verifica se o relatório está sendo enviado dentro do prazo
            var isBeforeDeadline = project.Notice?.FinalReportDeadline < DateTime.UtcNow;

            // Lança exceção caso o relatório esteja sendo enviado fora do prazo
            UseCaseException.BusinessRuleViolation(isBeforeDeadline,
                "Relatório enviado fora do prazo estipulado no edital.");

            // Atualiza o relatório na núvem
            await _storageFileService.UploadFileAsync(input.ReportFile!, report.ReportUrl);

            // Atualiza atributos permitidos
            report.UserId = user.Id;
            report.SendDate = DateTime.UtcNow;

            // Salva entidade atualizada no banco
            await _projectReportRepository.UpdateAsync(report);

            // Mapeia entidade para o modelo de saída e retorna
            return _mapper.Map<DetailedReadProjectFinalReportOutput>(report);
        }
    }
}