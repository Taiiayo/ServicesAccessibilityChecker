using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServicesAccessibilityChecker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    LastHourErrors = table.Column<int>(nullable: false),
                    LastDayErrors = table.Column<int>(nullable: false),
                    ResponseDuration = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ibonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    LastHourErrors = table.Column<int>(nullable: false),
                    LastDayErrors = table.Column<int>(nullable: false),
                    ResponseDuration = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ibonuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Refdatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    LastHourErrors = table.Column<int>(nullable: false),
                    LastDayErrors = table.Column<int>(nullable: false),
                    ResponseDuration = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refdatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Ibonuses");

            migrationBuilder.DropTable(
                name: "Refdatas");
        }
    }
}
