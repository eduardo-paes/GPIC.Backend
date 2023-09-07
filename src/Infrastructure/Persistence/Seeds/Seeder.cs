using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    static public class Seeder
    {
        public static void Execute(MigrationBuilder migrationBuilder)
        {
            ActivitiesSeeder.Seed(migrationBuilder);
            AreasSeeder.Seed(migrationBuilder);
            AssistanceTypeSeeder.Seed(migrationBuilder);
            CampusesSeeder.Seed(migrationBuilder);
            CoursesSeeder.Seed(migrationBuilder);
            ProgramTypesSeeder.Seed(migrationBuilder);
            UserSeeder.Seed(migrationBuilder);
        }
    }
}