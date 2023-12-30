using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketSlaGonfiguration_Column_Del_and_ForeignKey_to_Tickets_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketSlaId",
                table: "TicketSlaConfigurations");

            migrationBuilder.RenameColumn(
                name: "TicketSlaId",
                table: "Tickets",
                newName: "TicketSlaConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSlaConfigurations_TicketSlaConfigurationId",
                table: "Tickets",
                column: "TicketSlaConfigurationId",
                principalTable: "TicketSlaConfigurations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSlaConfigurations_TicketSlaConfigurationId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "TicketSlaConfigurationId",
                table: "Tickets",
                newName: "TicketSlaId");

            migrationBuilder.AddColumn<int>(
                name: "TicketSlaId",
                table: "TicketSlaConfigurations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
