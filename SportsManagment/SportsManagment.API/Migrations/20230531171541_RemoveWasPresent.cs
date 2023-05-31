using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWasPresent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasPresent",
                table: "TrainingAttendances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WasPresent",
                table: "TrainingAttendances",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
