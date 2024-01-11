using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_TicketService_related_columns_and_SubService_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketServicesConfigurations_TicketServices_ServiceId",
                table: "TicketServicesConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketServicesConfigurations",
                table: "TicketServicesConfigurations");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "TicketServicesConfigurations",
                newName: "DisplayOrderValue");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TicketServices",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "TicketStateId",
                table: "Tickets",
                newName: "TicketSubServiceId");

            migrationBuilder.AddColumn<int>(
                name: "TicketServiceId",
                table: "TicketServicesConfigurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketServicesConfigurations",
                table: "TicketServicesConfigurations",
                columns: new[] { "TicketTypeConfigurationId", "TicketServiceId" });

            migrationBuilder.CreateTable(
                name: "TicketSubService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketServiceId = table.Column<int>(type: "int", nullable: false),
                    SubServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrderValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSubService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSubService_TicketServices_TicketServiceId",
                        column: x => x.TicketServiceId,
                        principalTable: "TicketServices",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketServices_TicketServiceId",
                table: "Tickets",
                column: "TicketServiceId",
                principalTable: "TicketServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSubService_TicketSubServiceId",
                table: "Tickets",
                column: "TicketSubServiceId",
                principalTable: "TicketSubService",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketServicesConfigurations_TicketServices_TicketServiceId",
                table: "TicketServicesConfigurations",
                column: "TicketServiceId",
                principalTable: "TicketServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketServices_TicketServiceId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSubService_TicketSubServiceId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketServicesConfigurations_TicketServices_TicketServiceId",
                table: "TicketServicesConfigurations");

            migrationBuilder.DropTable(
                name: "TicketSubService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketServicesConfigurations",
                table: "TicketServicesConfigurations");

            migrationBuilder.DropColumn(
                name: "TicketServiceId",
                table: "TicketServicesConfigurations");

            migrationBuilder.RenameColumn(
                name: "DisplayOrderValue",
                table: "TicketServicesConfigurations",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "TicketServices",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TicketSubServiceId",
                table: "Tickets",
                newName: "TicketStateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketServicesConfigurations",
                table: "TicketServicesConfigurations",
                columns: new[] { "TicketTypeConfigurationId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TicketServicesConfigurations_TicketServices_ServiceId",
                table: "TicketServicesConfigurations",
                column: "ServiceId",
                principalTable: "TicketServices",
                principalColumn: "Id");
        }
    }
}
