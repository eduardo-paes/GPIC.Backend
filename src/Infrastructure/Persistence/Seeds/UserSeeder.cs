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
                        "123456",
                        "50806176083",
                        true,
                        0
                    },
                },
                schema: "public");
        }
    }
}