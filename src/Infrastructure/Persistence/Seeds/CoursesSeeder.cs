using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class CoursesSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            string coursesFile = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "courses.txt");
            foreach (string courseName in File.ReadAllLines(coursesFile))
            {
                // Save courses
                AddCourses(builder, new Course(Guid.NewGuid(), courseName.Trim()));
            }
        }

        private static void AddCourses(MigrationBuilder builder, Course m)
        {
            builder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "DeletedAt", "Name" },
                values: new object[,]
                {
                    { m.Id, m.DeletedAt, m.Name },
                },
                schema: "public");
        }
    }
}