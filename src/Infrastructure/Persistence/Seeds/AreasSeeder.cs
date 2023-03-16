using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public class AreasSeeder
    {
        public void Seed(MigrationBuilder builder)
        {
            string areasFile = Path.Combine(AppContext.BaseDirectory, "Seeds", "areas.txt");
            string[] lines = File.ReadAllLines(areasFile);
            string[] fields = new string[2];

            MainArea mainArea;
            Area area;
            SubArea subArea;
            foreach (string line in lines.Where(x => x.Contains("00.00.00")))
            {
                // Save main area
                fields = line.Split(';');
                mainArea = new MainArea(Guid.NewGuid(), fields[0].Trim(), fields[1].Trim());
                AddMainArea(builder, mainArea);
                Console.WriteLine(mainArea.Id);

                foreach (string subLine in lines.Where(x => x.Contains("00.00") && !x.Contains("00.00.00")))
                {
                    // Save area
                    fields = subLine.Split(';');
                    if (fields[0][..2] == mainArea.Code[..2])
                    {
                        area = new Area(Guid.NewGuid(), mainArea.Id, fields[0].Trim(), fields[1].Trim());
                        AddArea(builder, area);
                        Console.WriteLine(area.Id);

                        foreach (string subSubLine in lines.Where(x => x.Contains("00") && !x.Contains("00.00")))
                        {
                            // Save sub area
                            fields = subSubLine.Split(';');
                            if (fields[0].Substring(0, 4) == area.Code.Substring(0, 4))
                            {
                                subArea = new SubArea(Guid.NewGuid(), area.Id, fields[0].Trim(), fields[1].Trim());
                                AddSubArea(builder, subArea);
                                Console.WriteLine(subArea.Id);
                            }
                        }
                    }
                }
            }
        }

        private void AddMainArea(MigrationBuilder builder, MainArea m)
        {
            builder.InsertData(
                schema: "public",
                table: "MainAreas",
                columns: new[] { "Id", "Code", "DeletedAt", "Name" },
                values: new object[,]
                {
                    { m.Id, m.Code, m.DeletedAt, m.Name },
                });
        }

        private void AddArea(MigrationBuilder builder, Area a)
        {
            builder.InsertData(
                schema: "public",
                table: "Areas",
                columns: new[] { "Id", "Code", "DeletedAt", "Name", "MainAreaId" },
                values: new object[,]
                {
                    { a.Id, a.Code, a.DeletedAt, a.Name, a.MainAreaId },
                });
        }

        private void AddSubArea(MigrationBuilder builder, SubArea s)
        {
            builder.InsertData(
                schema: "public",
                table: "SubAreas",
                columns: new[] { "Id", "Code", "DeletedAt", "Name", "AreaId" },
                values: new object[,]
                {
                    { s.Id, s.Code, s.DeletedAt, s.Name, s.AreaId },
                });
        }
    }
}