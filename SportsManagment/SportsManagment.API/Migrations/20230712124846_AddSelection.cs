using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSelection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selection",
                table: "TrainingAttendances");

            migrationBuilder.AddColumn<Guid>(
                name: "SelectionId",
                table: "TrainingAttendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Selections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectionName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSelection",
                columns: table => new
                {
                    PlayersId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSelection", x => new { x.PlayersId, x.SelectionsId });
                    table.ForeignKey(
                        name: "FK_PlayerSelection_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSelection_Selections_SelectionsId",
                        column: x => x.SelectionsId,
                        principalTable: "Selections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingAttendances_SelectionId",
                table: "TrainingAttendances",
                column: "SelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSelection_SelectionsId",
                table: "PlayerSelection",
                column: "SelectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingAttendances_Selections_SelectionId",
                table: "TrainingAttendances",
                column: "SelectionId",
                principalTable: "Selections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingAttendances_Selections_SelectionId",
                table: "TrainingAttendances");

            migrationBuilder.DropTable(
                name: "PlayerSelection");

            migrationBuilder.DropTable(
                name: "Selections");

            migrationBuilder.DropIndex(
                name: "IX_TrainingAttendances_SelectionId",
                table: "TrainingAttendances");

            migrationBuilder.DropColumn(
                name: "SelectionId",
                table: "TrainingAttendances");

            migrationBuilder.AddColumn<int>(
                name: "Selection",
                table: "TrainingAttendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
