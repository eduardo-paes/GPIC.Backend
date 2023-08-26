using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Ports.ProjectFinalReport;
using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Validation;

namespace Application.UseCases.ProjectFinalReport
{
    public class CreateProjectFinalReport : ICreateProjectFinalReport
    {
        #region Global Scope
        private readonly IProjectFinalReportRepository _projectReportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStorageFileService _storageFileService;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IMapper _mapper;
        public CreateProjectFinalReport(IProjectFinalReportRepository projectReportRepository,
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

        public async Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(CreateProjectFinalReportInput input)
        {
            // Cria entidade a partir do modelo
            Domain.Entities.ProjectFinalReport report = new(
                input.ProjectId
            );

            // Obtém usuário logado
            var user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o projeto existe
            Domain.Entities.Project project = await _projectRepository.GetByIdAsync(report.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Project));

            // Verifica se o projeto foi excluído
            UseCaseException.BusinessRuleViolation(project.DeletedAt != null,
                "O projeto informado já foi removido.");

            // Verifica se o projeto está em andamento
            UseCaseException.BusinessRuleViolation(project.Status != Domain.Entities.Enums.EProjectStatus.Started,
                "O projeto informado não está em andamento.");

            // Somente aluno ou professor do projeto pode fazer inclusão de relatório
            UseCaseException.BusinessRuleViolation(user.Id != project.StudentId && user.Id != project.ProfessorId,
                "Somente o aluno ou o professor orientador do projeto pode fazer inclusão de relatório.");

            // Verifica se o relatório está sendo enviado dentro do prazo
            // Relatórios podem ser entregues até 6 meses antes do prazo final
            var isBeforeDeadline = project.Notice?.FinalReportDeadline <= DateTime.UtcNow
                && project.Notice?.FinalReportDeadline.Value.AddMonths(-6) >= DateTime.UtcNow;

            // Lança exceção caso o relatório esteja sendo enviado fora do prazo
            UseCaseException.BusinessRuleViolation(isBeforeDeadline,
                "Relatório enviado fora do prazo estipulado no edital.");

            // Tenta salvar o relatório no repositório de arquivos na núvem
            string? fileUrl = await _storageFileService.UploadFileAsync(input.ReportFile!);

            // Salva o link do arquivo no relatório
            report.ReportUrl = fileUrl;

            // Salva o Id do usuário logado no relatório
            report.UserId = user.Id;

            // Cria entidade
            report = await _projectReportRepository.CreateAsync(report);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadProjectFinalReportOutput>(report);
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