using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketPrioritiesGonfiguration_table_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketPrioritiesGonfiguration",
                columns: table => new
                {
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false),
                    TicketPriorityId = table.Column<int>(type: "int", nullable: false),
                    PriorityOrderValue = table.Column<int>(type: "int", nullable: false),
                    TicketSlaConfigurationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPrioritiesGonfiguration", x => new { x.TicketTypeConfigurationId, x.TicketPriorityId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPrioritiesGonfiguration");
        }
    }
}
