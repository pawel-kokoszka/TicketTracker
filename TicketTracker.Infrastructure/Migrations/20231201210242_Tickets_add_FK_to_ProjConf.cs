using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tickets_add_FK_to_ProjConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ProjectConfigurations_ProjectConfigurationId",
                table: "Tickets",
                column: "ProjectConfigurationId",
                principalTable: "ProjectConfigurations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ProjectConfigurations_ProjectConfigurationId",
                table: "Tickets");
        }
    }
}
