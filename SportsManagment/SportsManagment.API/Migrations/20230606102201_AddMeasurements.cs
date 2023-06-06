using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    HandSpan = table.Column<double>(type: "double precision", nullable: false),
                    ArmSpan = table.Column<double>(type: "double precision", nullable: false),
                    Skinfold = table.Column<double>(type: "double precision", nullable: false),
                    SitAndReach = table.Column<double>(type: "double precision", nullable: false),
                    ShoeSize = table.Column<double>(type: "double precision", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sprint20m = table.Column<double>(type: "double precision", nullable: false),
                    VerticalJump = table.Column<double>(type: "double precision", nullable: false),
                    BeepTest = table.Column<double>(type: "double precision", nullable: false),
                    AgilityTest505 = table.Column<double>(type: "double precision", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceMeasurements_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_PlayerId",
                table: "BodyMeasurements",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceMeasurements_PlayerId",
                table: "PerformanceMeasurements",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyMeasurements");

            migrationBuilder.DropTable(
                name: "PerformanceMeasurements");
        }
    }
}
