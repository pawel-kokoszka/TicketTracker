using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketState_related_FKs_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_TicketStatesGonfigurations_TicketStates_TicketStateId",
                table: "TicketStatesGonfigurations",
                column: "TicketStateId",
                principalTable: "TicketStates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketStatesGonfigurations_TicketTypeConfigurations_TicketTypeConfigurationId",
                table: "TicketStatesGonfigurations",
                column: "TicketTypeConfigurationId",
                principalTable: "TicketTypeConfigurations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketStatesGonfigurations_TicketStates_TicketStateId",
                table: "TicketStatesGonfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketStatesGonfigurations_TicketTypeConfigurations_TicketTypeConfigurationId",
                table: "TicketStatesGonfigurations");
        }
    }
}
