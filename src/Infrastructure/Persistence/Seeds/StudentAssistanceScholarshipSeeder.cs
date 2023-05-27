using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class StudentAssistanceScholarshipSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            string file = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "assistance-scolarship.txt");
            string[] parts;
            foreach (string lines in File.ReadAllLines(file))
            {
                parts = lines.Split(';');
                AddStudentAssistanceScholarship(builder, new StudentAssistanceScholarship(Guid.NewGuid(), parts[0], parts[1]));
            }
        }

        private static void AddStudentAssistanceScholarship(MigrationBuilder builder, StudentAssistanceScholarship sas)
        {
            if (sas?.Id == null || sas?.Name == null)
                return;
            builder.InsertData(
                table: "StudentAssistanceScholarships",
                columns: new[] { "Id", "DeletedAt", "Name", "Description" },
                values: new object[,]
                {
                    { sas.Id, null!, sas.Name, sas.Description ?? "" },
                },
                schema: "public");
        }
    }
}