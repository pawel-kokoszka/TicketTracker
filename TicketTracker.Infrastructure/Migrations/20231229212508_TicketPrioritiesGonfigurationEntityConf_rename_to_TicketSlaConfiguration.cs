using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketPrioritiesGonfigurationEntityConf_rename_to_TicketSlaConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPrioritiesConfigurations");

            migrationBuilder.CreateTable(
                name: "TicketSlaConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false),
                    PriorityOrderValue = table.Column<int>(type: "int", nullable: false),
                    TicketSlaId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSlaConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSlaConfigurations_TicketSLAs_TicketSlaId",
                        column: x => x.TicketSlaId,
                        principalTable: "TicketSLAs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketSlaConfigurations_TicketTypeConfigurations_TicketTypeConfigurationId",
                        column: x => x.TicketTypeConfigurationId,
                        principalTable: "TicketTypeConfigurations",
                        principalColumn: "Id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketSlaConfigurations");

            migrationBuilder.CreateTable(
                name: "TicketPrioritiesConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketSlaId = table.Column<int>(type: "int", nullable: false),
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false),
                    PriorityOrderValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPrioritiesConfigurations", x => x.Id);
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
        }
    }
}
