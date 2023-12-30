using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketSla_Table_Delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketSlaId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSlaConfigurations_TicketSLAs_TicketSlaId",
                table: "TicketSlaConfigurations");

            migrationBuilder.DropTable(
                name: "TicketSLAs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketSLAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    NumberOfMinutes = table.Column<int>(type: "int", nullable: false),
                    SlaName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSLAs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSLAs_TicketSlaId",
                table: "Tickets",
                column: "TicketSlaId",
                principalTable: "TicketSLAs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSlaConfigurations_TicketSLAs_TicketSlaId",
                table: "TicketSlaConfigurations",
                column: "TicketSlaId",
                principalTable: "TicketSLAs",
                principalColumn: "Id");
        }
    }
}
