using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketSLAs_FKs_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_TicketPrioritiesGonfiguration_TicketPriorities_TicketPriorityId",
                table: "TicketPrioritiesGonfiguration",
                column: "TicketPriorityId",
                principalTable: "TicketPriorities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPrioritiesGonfiguration_TicketSlaConfigurations_TicketSlaConfigurationId",
                table: "TicketPrioritiesGonfiguration",
                column: "TicketSlaConfigurationId",
                principalTable: "TicketSlaConfigurations",
                principalColumn: "TicketSlaConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPrioritiesGonfiguration_TicketTypeConfigurations_TicketTypeConfigurationId",
                table: "TicketPrioritiesGonfiguration",
                column: "TicketTypeConfigurationId",
                principalTable: "TicketTypeConfigurations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSlaConfigurations_TicketSLAs_TicketSlaId",
                table: "TicketSlaConfigurations",
                column: "TicketSlaId",
                principalTable: "TicketSLAs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPrioritiesGonfiguration_TicketPriorities_TicketPriorityId",
                table: "TicketPrioritiesGonfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPrioritiesGonfiguration_TicketSlaConfigurations_TicketSlaConfigurationId",
                table: "TicketPrioritiesGonfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPrioritiesGonfiguration_TicketTypeConfigurations_TicketTypeConfigurationId",
                table: "TicketPrioritiesGonfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSlaConfigurations_TicketSLAs_TicketSlaId",
                table: "TicketSlaConfigurations");
        }
    }
}
