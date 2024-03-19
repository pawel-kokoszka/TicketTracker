﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tickets_Add_Column_EditLockId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EditLockId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditLockId",
                table: "Tickets");
        }
    }
}
