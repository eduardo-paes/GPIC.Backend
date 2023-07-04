using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    static public class Seeder
    {
        public static void Seed(MigrationBuilder migrationBuilder)
        {
            AreasSeeder.Seed(migrationBuilder);
            CampusesSeeder.Seed(migrationBuilder);
            CoursesSeeder.Seed(migrationBuilder);
            ProgramTypesSeeder.Seed(migrationBuilder);
            StudentAssistanceScholarshipSeeder.Seed(migrationBuilder);
            UserSeeder.Seed(migrationBuilder);
        }
    }
}