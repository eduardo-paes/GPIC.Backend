using Application.Interfaces.UseCases.Notice;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases.Notice
{
    public class ReportDeadlineNotification : IReportDeadlineNotification
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly IEmailService _emailService;
        public ReportDeadlineNotification(
            IProjectRepository projectRepository,
            IEmailService emailService)
        {
            _projectRepository = projectRepository;
            _emailService = emailService;
        }
        #endregion

        public async Task<string> ExecuteAsync()
        {
            // Verifica se o há projetos que estejam no status Iniciado
            var projects = await _projectRepository.GetProjectsWithCloseReportDueDateAsync();
            if (!projects.Any())
                return "Nenhum projeto com prazo de entrega de relatório próxima.";

            // Define datas de comparação
            DateTime nextMonth = DateTime.UtcNow.AddMonths(1).Date;
            DateTime nextWeek = DateTime.UtcNow.AddDays(7).Date;

            // Envia notificação para cada projeto
            foreach (var project in projects)
            {
                // Verifica qual o relatório com data de entrega mais próxima
                DateTime reportDeadline = project.Notice!.PartialReportDeadline!.Value.Date;
                string reportType;
                if (reportDeadline == nextWeek || reportDeadline == nextMonth)
                {
                    reportType = "Relatório Parcial";
                }
                else
                {
                    reportType = "Relatório Final";
                    reportDeadline = project.Notice!.FinalReportDeadline!.Value.Date;
                }

                // Envia notificação para o professor
                await _emailService.SendNotificationOfReportDeadlineEmailAsync(
                    project.Professor!.User!.Email,
                    project.Professor.User.Name,
                    project.Title,
                    reportType,
                    reportDeadline
                );
            }

            return "Notificação de prazo de entrega de relatório enviada com sucesso.";
        }
    }
}