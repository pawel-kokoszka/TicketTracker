using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeamRoleTypes_and_TeamsRoles_Add_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamRoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamsRoles",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsRoles", x => new { x.TeamId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TeamsRoles_TeamRoleTypes_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TeamRoleTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamsRoles_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamsRoles_TicketTypeConfigurations_TicketTypeConfigurationId",
                        column: x => x.TicketTypeConfigurationId,
                        principalTable: "TicketTypeConfigurations",
                        principalColumn: "Id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamsRoles");

            migrationBuilder.DropTable(
                name: "TeamRoleTypes");
        }
    }
}
