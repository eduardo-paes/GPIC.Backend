using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class AssistanceTypeSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            string file = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "assistance-scolarship.txt");
            string[] parts;
            foreach (string lines in File.ReadAllLines(file))
            {
                parts = lines.Split(';');
                AddAssistanceType(builder, new AssistanceType(Guid.NewGuid(), parts[0], parts[1]));
            }
        }

        private static void AddAssistanceType(MigrationBuilder builder, AssistanceType sas)
        {
            if (sas?.Id == null || sas?.Name == null)
                return;
            builder.InsertData(
                table: "AssistanceTypes",
                columns: new[] { "Id", "DeletedAt", "Name", "Description" },
                values: new object[,]
                {
                    { sas.Id, null!, sas.Name, sas.Description ?? "" },
                },
                schema: "public");
        }
    }
}