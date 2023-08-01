using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations;

/// <inheritdoc />
public partial class AddTrainingAttendances : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TrainingAttendances",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Date = table.Column<DateOnly>(type: "date", nullable: false),
                WasPresent = table.Column<bool>(type: "boolean", nullable: false),
                Selection = table.Column<int>(type: "integer", nullable: false),
                PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TrainingAttendances", x => x.Id);
                table.ForeignKey(
                    name: "FK_TrainingAttendances_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_TrainingAttendances_PlayerId",
            table: "TrainingAttendances",
            column: "PlayerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TrainingAttendances");
    }
}
