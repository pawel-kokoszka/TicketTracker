using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketTypeConfiguration_FKs_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_TicketTypeConfigurations_ProjectConfigurations_ProjectConfigurationId",
                table: "TicketTypeConfigurations",
                column: "ProjectConfigurationId",
                principalTable: "ProjectConfigurations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTypeConfigurations_TicketTypes_TicketTypeId",
                table: "TicketTypeConfigurations",
                column: "TicketTypeId",
                principalTable: "TicketTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTypeConfigurations_ProjectConfigurations_ProjectConfigurationId",
                table: "TicketTypeConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTypeConfigurations_TicketTypes_TicketTypeId",
                table: "TicketTypeConfigurations");
        }
    }
}
