using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketService_FKs_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_TicketServicesConfigurations_TicketServices_ServiceId",
                table: "TicketServicesConfigurations",
                column: "ServiceId",
                principalTable: "TicketServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketServicesConfigurations_TicketTypeConfigurations_TicketTypeConfigurationId",
                table: "TicketServicesConfigurations",
                column: "TicketTypeConfigurationId",
                principalTable: "TicketTypeConfigurations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketServicesConfigurations_TicketServices_ServiceId",
                table: "TicketServicesConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketServicesConfigurations_TicketTypeConfigurations_TicketTypeConfigurationId",
                table: "TicketServicesConfigurations");
        }
    }
}
