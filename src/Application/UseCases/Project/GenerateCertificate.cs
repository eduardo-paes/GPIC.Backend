using Application.Interfaces.UseCases.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Project
{
    public class GenerateCertificate : IGenerateCertificate
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly INoticeRepository _noticeRepository;
        private readonly IProjectFinalReportRepository _projectReportRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReportService _reportService;
        private readonly IStorageFileService _storageFileService;
        public GenerateCertificate(
            IProjectRepository projectRepository,
            INoticeRepository noticeRepository,
            IProjectFinalReportRepository projectReportRepository,
            IProfessorRepository professorRepository,
            IUserRepository userRepository,
            IReportService reportService,
            IStorageFileService storageFileService)
        {
            _projectRepository = projectRepository;
            _noticeRepository = noticeRepository;
            _projectReportRepository = projectReportRepository;
            _professorRepository = professorRepository;
            _userRepository = userRepository;
            _reportService = reportService;
            _storageFileService = storageFileService;
        }
        #endregion

        public async Task<string> ExecuteAsync()
        {
            // Busca edital que possui data final de entrega de relatório para o dia anterior
            var notice = await _noticeRepository.GetNoticeEndingAsync();
            if (notice is null)
                return "Nenhum edital em estágio de encerramento encontrado.";

            // Verifica se o há projetos que estejam no status Started ou Pending
            var projects = await _projectRepository.GetProjectByNoticeAsync(notice!.Id);
            if (!projects.Any())
                return "Nenhum projeto em estágio de encerramento encontrado.";

            // Obtém informações do coordenador
            var coordinator = await _userRepository.GetCoordinatorAsync();
            if (coordinator is null)
                return "Nenhum coordenador encontrado.";

            // Encerra cada projeto verificando se o mesmo possui relatório final
            foreach (var project in projects)
            {
                // Verifica se o projeto possui relatório final
                var finalReport = await _projectReportRepository.GetByProjectIdAsync(project.Id);

                // Se não possuir relatório final, não gera certificado e suspende o professor
                if (finalReport is null)
                {
                    // Suspende professor
                    var professor = await _professorRepository.GetByIdAsync(project.ProfessorId);
                    if (professor is not null)
                    {
                        professor.SuspensionEndDate = DateTime.Now.AddYears(notice.SuspensionYears ?? 0);
                        await _professorRepository.UpdateAsync(professor);
                    }

                    // Encerra projeto
                    project.Status = EProjectStatus.Closed;
                    await _projectRepository.UpdateAsync(project);
                }
                else
                {
                    // Gera nome único para o arquivo
                    var uniqueName = Guid.NewGuid().ToString() + ".pdf";

                    // Gera certificado
                    var path = await _reportService.GenerateCertificateAsync(project, coordinator.Name!, uniqueName);

                    // Converte certificado para IFormFile a fim de enviá-lo para nuvem
                    var bytes = File.ReadAllBytes(path);

                    // Envia certificado para nuvem e salva url no projeto
                    project.CertificateUrl = await _storageFileService.UploadFileAsync(bytes, path);

                    // Encerra projeto
                    project.Status = EProjectStatus.Closed;
                    await _projectRepository.UpdateAsync(project);

                    // Deleta certificado da pasta temporária
                    File.Delete(path);
                }
            }

            return "Certificados gerados com sucesso.";
        }

        private async Task<string> TestMethod()
        {
            var project = new Domain.Entities.Project("Project Title", "Keyword 1", "Keyword 2", "Keyword 3", true, "Objective", "Methodology", "Expected Results", "Schedule", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), EProjectStatus.Opened, "Status Description", "Appeal Observation", DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, "Cancellation Reason");
            var uniqueName = Guid.NewGuid().ToString() + ".pdf";
            var path = await _reportService.GenerateCertificateAsync(project, "Eduardo Paes", uniqueName);

            // Converte certificado para IFormFile a fim de enviá-lo para nuvem
            var bytes = File.ReadAllBytes(path);

            // Envia certificado para nuvem e salva url no projeto
            var url = await _storageFileService.UploadFileAsync(bytes, path);

            // Mostra URL do certificado
            Console.WriteLine(url);

            return "Certificado gerado com sucesso. Url: " + url;
        }
    }
}