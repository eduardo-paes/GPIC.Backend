using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.UseCases.Project;
using Application.Validation;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http.Internal;

namespace Application.UseCases.Project
{
    public class GenerateCertificate : IGenerateCertificate
    {
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
                var reports = await _projectReportRepository.GetByProjectIdAsync(project.Id);

                // Se não possuir relatório final, não gera certificado e suspende o professor
                if (reports is null || !reports.Any())
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
                    var file = new FormFile(new MemoryStream(File.ReadAllBytes(path)), 0, 0, "certificate", uniqueName);

                    // Envia certificado para nuvem e salva url no projeto
                    project.CertificateUrl = await _storageFileService.UploadFileAsync(file);

                    // Encerra projeto
                    project.Status = EProjectStatus.Closed;
                    await _projectRepository.UpdateAsync(project);

                    // Deleta certificado da pasta temporária
                    File.Delete(path);
                }
            }

            return "Certificados gerados com sucesso.";
        }
    }
}