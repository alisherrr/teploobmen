using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploobmenWeb.Migrations
{
    public partial class miginit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    InitTemperatureMaterial = table.Column<double>(type: "REAL", nullable: false),
                    InitTemperatureGas = table.Column<double>(type: "REAL", nullable: false),
                    SpeedGas = table.Column<double>(type: "REAL", nullable: false),
                    MiddleHeatСapacity = table.Column<double>(type: "REAL", nullable: false),
                    ConsumptionMaterial = table.Column<double>(type: "REAL", nullable: false),
                    HeatСapacityMaterial = table.Column<double>(type: "REAL", nullable: false),
                    VolumetricHeatTransferCoefficient = table.Column<double>(type: "REAL", nullable: false),
                    ApparatusDiameter = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Variants");
        }
    }
}
