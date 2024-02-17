using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Teams_Tickets_FK_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Teams_AssignedTeamId",
                table: "Tickets",
                column: "AssignedTeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Teams_AssigningTeamId",
                table: "Tickets",
                column: "AssigningTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Teams_AssignedTeamId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Teams_AssigningTeamId",
                table: "Tickets");
        }
    }
}
