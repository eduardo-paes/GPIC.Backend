using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Ports.ProjectReport;
using Domain.UseCases.Interfaces.ProjectReport;
using Domain.UseCases.Ports.ProjectReport;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProjectReport
{
    public class CreateProjectReport : ICreateProjectReport
    {
        #region Global Scope
        private readonly IProjectReportRepository _projectReportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStorageFileService _storageFileService;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IMapper _mapper;
        public CreateProjectReport(IProjectReportRepository projectReportRepository,
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

        public async Task<DetailedReadProjectReportOutput> ExecuteAsync(CreateProjectReportInput input)
        {
            // Cria entidade a partir do modelo
            Entities.ProjectReport report = new(
                TryCastEnum<Entities.Enums.EReportType>(input.ReportType),
                input.ProjectId
            );

            // Obtém usuário logado
            Ports.Auth.UserClaimsOutput user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o projeto existe
            Entities.Project project = await _projectRepository.GetByIdAsync(report.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o projeto foi excluído
            UseCaseException.BusinessRuleViolation(project.DeletedAt != null,
                "O projeto informado já foi removido.");

            // Verifica se o projeto está em andamento
            UseCaseException.BusinessRuleViolation(project.Status != Entities.Enums.EProjectStatus.Started,
                "O projeto informado não está em andamento.");

            // Verifica se o relatório está sendo enviado dentro do prazo
            var isBeforeDeadline = report.ReportType == Entities.Enums.EReportType.Final
                ? project.Notice?.FinalReportDeadline < DateTime.UtcNow
                : project.Notice?.PartialReportDeadline < DateTime.UtcNow;

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
            return _mapper.Map<DetailedReadProjectReportOutput>(report);
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