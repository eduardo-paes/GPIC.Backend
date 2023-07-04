using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class UserSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            builder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DeletedAt", "Name", "Email", "Password", "CPF", "IsConfirmed", "Role" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        null!,
                        "Root Admin",
                        "edu-paes@hotmail.com",
                        "ieSgcgP4w2Am80FsWXlCqg==.4F9CiQ8v2t4Mu62R/DVJILBtQrl8mPh73MlbogRMXkw=",
                        "50806176083",
                        true,
                        0
                    },
                },
                schema: "public");
        }
    }
}