using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations;

/// <inheritdoc />
public partial class AddPlayerMeasurement : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "MeasurementDate",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MeasurementDate", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PlayerMeasurements",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Sprint20m = table.Column<double>(type: "double precision", nullable: false),
                VerticalJump = table.Column<double>(type: "double precision", nullable: false),
                BeepTest = table.Column<double>(type: "double precision", nullable: false),
                AgilityTest505 = table.Column<double>(type: "double precision", nullable: false),
                Weight = table.Column<double>(type: "double precision", nullable: false),
                Height = table.Column<double>(type: "double precision", nullable: false),
                HandSpan = table.Column<double>(type: "double precision", nullable: false),
                ArmSpan = table.Column<double>(type: "double precision", nullable: false),
                Skinfold = table.Column<double>(type: "double precision", nullable: false),
                SitAndReach = table.Column<double>(type: "double precision", nullable: false),
                ShoeSize = table.Column<double>(type: "double precision", nullable: false),
                PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                MeasurementDateId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlayerMeasurements", x => x.Id);
                table.ForeignKey(
                    name: "FK_PlayerMeasurements_MeasurementDate_MeasurementDateId",
                    column: x => x.MeasurementDateId,
                    principalTable: "MeasurementDate",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PlayerMeasurements_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PlayerMeasurements_MeasurementDateId",
            table: "PlayerMeasurements",
            column: "MeasurementDateId");

        migrationBuilder.CreateIndex(
            name: "IX_PlayerMeasurements_PlayerId",
            table: "PlayerMeasurements",
            column: "PlayerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PlayerMeasurements");

        migrationBuilder.DropTable(
            name: "MeasurementDate");
    }
}
