using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class ProgramTypesSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            string file = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "program-type.txt");
            string[] parts;
            foreach (string lines in File.ReadAllLines(file))
            {
                parts = lines.Split(';');
                AddProgramTypes(builder, new ProgramType(Guid.NewGuid(), parts[0], parts[1]));
            }
        }

        private static void AddProgramTypes(MigrationBuilder builder, ProgramType pt)
        {
            if (pt?.Id == null || pt?.Name == null)
                return;
            builder.InsertData(
                table: "ProgramTypes",
                columns: new[] { "Id", "DeletedAt", "Name", "Description" },
                values: new object[,]
                {
                    { pt.Id, null!, pt.Name, pt.Description ?? "" },
                },
                schema: "public");
        }
    }
}