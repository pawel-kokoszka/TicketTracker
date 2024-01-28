using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ticket_Entity_Property_Typo_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssingningTeamId",
                table: "Tickets",
                newName: "AssigningTeamId");

            migrationBuilder.RenameColumn(
                name: "AssingedTeamId",
                table: "Tickets",
                newName: "AssignedTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssigningTeamId",
                table: "Tickets",
                newName: "AssingningTeamId");

            migrationBuilder.RenameColumn(
                name: "AssignedTeamId",
                table: "Tickets",
                newName: "AssingedTeamId");
        }
    }
}
