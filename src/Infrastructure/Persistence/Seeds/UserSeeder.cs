using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class UserSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            var userId = Guid.NewGuid();

            builder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DeletedAt", "Name", "Email", "Password", "CPF", "IsConfirmed", "IsCoordinator", "Role" },
                values: new object[,]
                {
                    {
                        userId,
                        null!,
                        "Root Admin",
                        "edu-paes@hotmail.com",
                        "ieSgcgP4w2Am80FsWXlCqg==.4F9CiQ8v2t4Mu62R/DVJILBtQrl8mPh73MlbogRMXkw=",
                        "50806176083",
                        true,
                        true,
                        0
                    },
                },
                schema: "public");

            builder.InsertData(
                table: "Professors",
                columns: new[] { "Id", "DeletedAt", "SuspensionEndDate", "IdentifyLattes", "SIAPEEnrollment", "UserId" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        null!,
                        null!,
                        "1234567",
                        1234567,
                        userId
                    },
                },
                schema: "public");
        }
    }
}