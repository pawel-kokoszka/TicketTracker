using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_TicketSlaConfiguration_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPrioritiesGonfiguration");

            migrationBuilder.DropTable(
                name: "TicketSlaConfigurations");

            migrationBuilder.DropColumn(
                name: "IsDeadline",
                table: "TicketSLAs");

            migrationBuilder.CreateTable(
                name: "TicketPrioritiesConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false),
                    TicketPriorityId = table.Column<int>(type: "int", nullable: false),
                    PriorityOrderValue = table.Column<int>(type: "int", nullable: false),
                    TicketSlaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPrioritiesConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketPrioritiesConfigurations_TicketPriorities_TicketPriorityId",
                        column: x => x.TicketPriorityId,
                        principalTable: "TicketPriorities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketPrioritiesConfigurations_TicketSLAs_TicketSlaId",
                        column: x => x.TicketSlaId,
                        principalTable: "TicketSLAs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketPrioritiesConfigurations_TicketTypeConfigurations_TicketTypeConfigurationId",
                        column: x => x.TicketTypeConfigurationId,
                        principalTable: "TicketTypeConfigurations",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketPriorityId",
                table: "Tickets",
                column: "TicketPriorityId",
                principalTable: "TicketSLAs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketPriorityId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketPrioritiesConfigurations");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeadline",
                table: "TicketSLAs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TicketSlaConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketSlaId = table.Column<int>(type: "int", nullable: false),
                    TicketSlaConfigurationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSlaConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSlaConfigurations_TicketSLAs_TicketSlaId",
                        column: x => x.TicketSlaId,
                        principalTable: "TicketSLAs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketPrioritiesGonfiguration",
                columns: table => new
                {
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false),
                    TicketPriorityId = table.Column<int>(type: "int", nullable: false),
                    TicketSlaConfigurationId = table.Column<int>(type: "int", nullable: false),
                    PriorityOrderValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPrioritiesGonfiguration", x => new { x.TicketTypeConfigurationId, x.TicketPriorityId });
                    table.ForeignKey(
                        name: "FK_TicketPrioritiesGonfiguration_TicketPriorities_TicketPriorityId",
                        column: x => x.TicketPriorityId,
                        principalTable: "TicketPriorities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketPrioritiesGonfiguration_TicketSlaConfigurations_TicketSlaConfigurationId",
                        column: x => x.TicketSlaConfigurationId,
                        principalTable: "TicketSlaConfigurations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketPrioritiesGonfiguration_TicketTypeConfigurations_TicketTypeConfigurationId",
                        column: x => x.TicketTypeConfigurationId,
                        principalTable: "TicketTypeConfigurations",
                        principalColumn: "Id");
                });
        }
    }
}
