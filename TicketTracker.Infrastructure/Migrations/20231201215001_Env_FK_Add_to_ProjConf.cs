using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Env_FK_Add_to_ProjConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ProjectConfigurations_Environments_EnvironmentId",
                table: "ProjectConfigurations",
                column: "EnvironmentId",
                principalTable: "Environments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectConfigurations_Environments_EnvironmentId",
                table: "ProjectConfigurations");
        }
    }
}
