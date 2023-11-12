using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMonthlyFeePaid",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "IsYearlyFeePaid",
                table: "Players",
                newName: "IsDeleted");

            migrationBuilder.CreateTable(
                name: "PaymentInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfPayment = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentInformations_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInformations_PlayerId",
                table: "PaymentInformations",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentInformations");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Players",
                newName: "IsYearlyFeePaid");

            migrationBuilder.AddColumn<bool>(
                name: "IsMonthlyFeePaid",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
