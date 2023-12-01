using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Env_and_EnvTypes_FK_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Environments_EnvironmentTypes_EnvironmentTypeId",
                table: "Environments",
                column: "EnvironmentTypeId",
                principalTable: "EnvironmentTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environments_EnvironmentTypes_EnvironmentTypeId",
                table: "Environments");
        }
    }
}
