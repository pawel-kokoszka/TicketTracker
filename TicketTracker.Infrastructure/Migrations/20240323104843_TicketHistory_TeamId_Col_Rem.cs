using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketHistory_TeamId_Col_Rem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_Teams_TeamId",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TicketHistory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "TicketHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_Teams_TeamId",
                table: "TicketHistory",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
