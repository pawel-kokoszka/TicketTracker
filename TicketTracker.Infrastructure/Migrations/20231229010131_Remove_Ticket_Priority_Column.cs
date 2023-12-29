using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Ticket_Priority_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketPriorityId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketPriorityId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketSlaId",
                table: "Tickets",
                column: "TicketSlaId",
                principalTable: "TicketSLAs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketSlaId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketPriorityId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketPriorityId",
                table: "Tickets",
                column: "TicketPriorityId",
                principalTable: "TicketSLAs",
                principalColumn: "Id");
        }
    }
}
