using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class ActivitiesSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            // Carrega tipos de atividades
            string activityTypePath = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "activity-types.txt");
            string[] activityTypeLines = File.ReadAllLines(activityTypePath);

            // Carrega atividades
            string activitiesPath = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "activities.txt");
            string[] activitiesLines = File.ReadAllLines(activitiesPath);

            // Cria edital base no banco
            var noticeId = AddDefaultNotice(builder);

            // Cria tipos de atividades e atividades
            foreach (string activityTypeLine in activityTypeLines)
            {
                // Carrega dados do tipo de atividade
                var codeAT = activityTypeLine.Split(";")[0].Trim();
                var nameAT = activityTypeLine.Split(";")[1].Trim();
                var unityAT = activityTypeLine.Split(";")[2].Trim();

                // Cria tipo de atividade no banco
                var newActivityTypeId = AddActivityType(builder, nameAT, unityAT, noticeId);

                // Cria atividades
                foreach (var activity in activitiesLines.Where(a => a.Split(";")[0] == codeAT))
                {
                    // Carrega dados da atividade
                    var codeA = activity.Split(";")[0].Trim();
                    var nameA = activity.Split(";")[1].Trim();
                    var pointsA = activity.Split(";")[2].Trim();
                    var limitsA = activity.Split(";")[3].Trim();

                    // Trata valores nulos
                    if (limitsA == "NULL")
                        limitsA = null;

                    // Cria atividade no banco
                    AddActivity(builder, newActivityTypeId, nameA, pointsA, limitsA);
                }
            }
        }

        private static Guid AddDefaultNotice(MigrationBuilder builder)
        {
            var newNoticeId = Guid.NewGuid();
            builder.InsertData(
                table: "Notices",
                columns: new[]
                {
                    "Id", "DeletedAt",
                    "RegistrationStartDate", "RegistrationEndDate",
                    "EvaluationStartDate", "EvaluationEndDate",
                    "AppealStartDate", "AppealEndDate",
                    "SendingDocsStartDate", "SendingDocsEndDate",
                    "PartialReportDeadline", "FinalReportDeadline",
                    "SuspensionYears", "DocUrl", "CreatedAt", "Description"
                },
                values: new object[,]
                {
                    {
                        newNoticeId, // Id
                        null!, // Deleted At
                        new DateTime(1990, 8, 1).ToUniversalTime(), // Registration Start Date
                        new DateTime(1990, 8, 31).ToUniversalTime(), // Registration End Date
                        new DateTime(1990, 9, 15).ToUniversalTime(), // Evaluation Start Date
                        new DateTime(1990, 9, 30).ToUniversalTime(), // Evaluation End Date
                        new DateTime(1990, 10, 10).ToUniversalTime(), // Appeal Start Date
                        new DateTime(1990, 10, 15).ToUniversalTime(), // Appeal End Date
                        new DateTime(1990, 11, 1).ToUniversalTime(), // Sending Docs Start Date
                        new DateTime(1990, 11, 15).ToUniversalTime(), // Sending Docs End Date
                        new DateTime(1991, 1, 15).ToUniversalTime(), // Partial Report Deadline
                        new DateTime(1991, 3, 15).ToUniversalTime(), // Final Report Deadline
                        1, // Suspension Years
                        null!, // Doc URL
                        DateTime.UtcNow, // Created At
                        "Edital de Inicialização", // Description
                    },
                },
                schema: "public");
            return newNoticeId;
        }

        private static Guid AddActivityType(MigrationBuilder builder, string name, string unity, Guid noticeId)
        {
            var newActivityTypeId = Guid.NewGuid();

            builder.InsertData(
                table: "ActivityTypes",
                columns: new[] { "Id", "DeletedAt", "Name", "Unity", "NoticeId" },
                values: new object[,]
                {
                    { newActivityTypeId, null!, name, unity, noticeId },
                },
                schema: "public");

            return newActivityTypeId;
        }

        private static void AddActivity(MigrationBuilder builder, Guid newActivityTypeId, string name, string points, string? limits)
        {
            builder.InsertData(
                table: "Activities",
                columns: new[]
                {
                    "Id", "DeletedAt", "Name", "Points", "Limits", "ActivityTypeId"
                },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        null!,
                        name,
                        points,
                        limits!,
                        newActivityTypeId
                    },
                },
                schema: "public");
        }
    }
}