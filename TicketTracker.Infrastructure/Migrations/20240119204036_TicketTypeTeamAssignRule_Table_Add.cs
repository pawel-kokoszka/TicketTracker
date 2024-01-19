using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketTypeTeamAssignRule_Table_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "displayOrder",
                table: "UserTeamConfigurations",
                newName: "DisplayOrder");

            migrationBuilder.CreateTable(
                name: "TicketTypeTeamAssignRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false),
                    AssigningTeamId = table.Column<int>(type: "int", nullable: false),
                    AssignedTeamId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserTeamConfigurationsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypeTeamAssignRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTypeTeamAssignRules_TicketTypeConfigurations_TicketTypeConfigurationId",
                        column: x => x.TicketTypeConfigurationId,
                        principalTable: "TicketTypeConfigurations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTypeTeamAssignRules_UserTeamConfigurations_AssignedTeamId",
                        column: x => x.AssignedTeamId,
                        principalTable: "UserTeamConfigurations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTypeTeamAssignRules_UserTeamConfigurations_AssignedUserTeamConfigurationsId",
                        column: x => x.AssignedUserTeamConfigurationsId,
                        principalTable: "UserTeamConfigurations",
                        principalColumn: "Id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketTypeTeamAssignRules");

            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "UserTeamConfigurations",
                newName: "displayOrder");
        }
    }
}
