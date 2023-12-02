using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketState_related_tables_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatesGonfigurations",
                columns: table => new
                {
                    TicketTypeConfigurationId = table.Column<int>(type: "int", nullable: false),
                    TicketStateId = table.Column<int>(type: "int", nullable: false),
                    TicketStateOrderValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatesGonfigurations", x => new { x.TicketTypeConfigurationId, x.TicketStateId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketStates");

            migrationBuilder.DropTable(
                name: "TicketStatesGonfigurations");
        }
    }
}
