using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Seeds
{
    public static class AreasSeeder
    {
        public static void Seed(MigrationBuilder builder)
        {
            string areasFile = Path.Combine(AppContext.BaseDirectory, "Seeds", "Data", "areas.txt");
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

                foreach (string subLine in lines.Where(x => x.Contains("00.00") && !x.Contains("00.00.00")))
                {
                    if (mainArea?.Code == null)
                        continue;

                    // Save area
                    fields = subLine.Split(';');
                    if (fields[0][..2] == mainArea?.Code[..2])
                    {
                        area = new Area(Guid.NewGuid(), mainArea.Id, fields[0].Trim(), fields[1].Trim());
                        AddArea(builder, area);

                        foreach (string subSubLine in lines.Where(x => x.Contains("00") && !x.Contains("00.00")))
                        {
                            if (area?.Code == null)
                                continue;

                            // Save sub area
                            fields = subSubLine.Split(';');
                            if (fields[0][..4] == area?.Code[..4])
                            {
                                subArea = new SubArea(Guid.NewGuid(), area.Id, fields[0].Trim(), fields[1].Trim());
                                AddSubArea(builder, subArea);
                            }
                        }
                    }
                }
            }
        }

        private static void AddMainArea(MigrationBuilder builder, MainArea m)
        {
            if (m?.Id == null || m?.Code == null || m?.Name == null)
                return;
            builder.InsertData(
            table: "MainAreas",
            columns: new[] { "Id", "Code", "DeletedAt", "Name" },
            values: new object[,]
            {
                { m.Id, m.Code, null!, m.Name },
            },
            schema: "public");
        }

        private static void AddArea(MigrationBuilder builder, Area a)
        {
            if (a?.Id == null || a?.Code == null || a?.Name == null || a?.MainAreaId == null)
                return;
            builder.InsertData(
            table: "Areas",
            columns: new[] { "Id", "Code", "DeletedAt", "Name", "MainAreaId" },
            values: new object[,]
            {
                { a.Id, a.Code, null!, a.Name, a.MainAreaId },
            },
            schema: "public");
        }

        private static void AddSubArea(MigrationBuilder builder, SubArea s)
        {
            if (s?.Id == null || s?.Code == null || s?.Name == null || s?.AreaId == null)
                return;
            builder.InsertData(
            table: "SubAreas",
            columns: new[] { "Id", "Code", "DeletedAt", "Name", "AreaId" },
            values: new object[,]
            {
                { s.Id, s.Code, null!, s.Name, s.AreaId },
            },
            schema: "public");
        }
    }
}