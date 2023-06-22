using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasurementInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMeasurements_MeasurementDate_MeasurementDateId",
                table: "PlayerMeasurements");

            migrationBuilder.DropTable(
                name: "MeasurementDate");

            migrationBuilder.RenameColumn(
                name: "MeasurementDateId",
                table: "PlayerMeasurements",
                newName: "MeasurementInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerMeasurements_MeasurementDateId",
                table: "PlayerMeasurements",
                newName: "IX_PlayerMeasurements_MeasurementInformationId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "VerticalJump",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Sprint20m",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Skinfold",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "SitAndReach",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShoeSize",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "HandSpan",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "BeepTest",
                table: "PlayerMeasurements",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "ArmSpan",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "AgilityTest505",
                table: "PlayerMeasurements",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateTable(
                name: "MeasurementInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementInformations", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMeasurements_MeasurementInformations_MeasurementInfor~",
                table: "PlayerMeasurements",
                column: "MeasurementInformationId",
                principalTable: "MeasurementInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMeasurements_MeasurementInformations_MeasurementInfor~",
                table: "PlayerMeasurements");

            migrationBuilder.DropTable(
                name: "MeasurementInformations");

            migrationBuilder.RenameColumn(
                name: "MeasurementInformationId",
                table: "PlayerMeasurements",
                newName: "MeasurementDateId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerMeasurements_MeasurementInformationId",
                table: "PlayerMeasurements",
                newName: "IX_PlayerMeasurements_MeasurementDateId");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "VerticalJump",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Sprint20m",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Skinfold",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "SitAndReach",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "ShoeSize",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "HandSpan",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "BeepTest",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "ArmSpan",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "AgilityTest505",
                table: "PlayerMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMeasurements_MeasurementDate_MeasurementDateId",
                table: "PlayerMeasurements",
                column: "MeasurementDateId",
                principalTable: "MeasurementDate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
