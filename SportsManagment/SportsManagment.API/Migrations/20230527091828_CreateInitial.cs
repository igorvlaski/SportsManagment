﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsManagment.API.Migrations;

/// <inheritdoc />
public partial class CreateInitial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Players",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "text", nullable: true),
                LastName = table.Column<string>(type: "text", nullable: true),
                DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                IsMonthlyFeePaid = table.Column<bool>(type: "boolean", nullable: false),
                IsYearlyFeePaid = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Players", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Players");
    }
}
