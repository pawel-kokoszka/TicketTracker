using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketSla_Column_Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlaValue",
                table: "TicketSLAs");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeadline",
                table: "TicketSLAs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "TicketSLAs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMinutes",
                table: "TicketSLAs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeadline",
                table: "TicketSLAs");

            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "TicketSLAs");

            migrationBuilder.DropColumn(
                name: "NumberOfMinutes",
                table: "TicketSLAs");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SlaValue",
                table: "TicketSLAs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
