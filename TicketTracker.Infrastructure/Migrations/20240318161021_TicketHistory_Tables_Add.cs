using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketHistory_Tables_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EditLockId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    SummaryComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketHistory_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketHistory_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketHistoryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketHistoryId = table.Column<int>(type: "int", nullable: false),
                    TicketPropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyNewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyOldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketHistoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketHistoryDetails_TicketHistory_TicketHistoryId",
                        column: x => x.TicketHistoryId,
                        principalTable: "TicketHistory",
                        principalColumn: "Id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketHistoryDetails");

            migrationBuilder.DropTable(
                name: "TicketHistory");
        }
    }
}
