using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public class CoursesSeeder
    {
        public void Seed(MigrationBuilder builder)
        {
            string coursesFile = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "courses.txt");
            foreach (string courseName in File.ReadAllLines(coursesFile))
            {
                // Save courses
                AddCourses(builder, new Course(Guid.NewGuid(), courseName.Trim()));
            }
        }

        private void AddCourses(MigrationBuilder builder, Course m)
        {
            builder.InsertData(
                schema: "public",
                table: "Courses",
                columns: new[] { "Id", "DeletedAt", "Name" },
                values: new object[,]
                {
                    { m.Id, m.DeletedAt, m.Name },
                });
        }
    }
}