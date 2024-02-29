using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketFlowConfiguration_column_UserCanChangeStatus_ADD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AssignedUserCanChangeStatus",
                table: "TicketFlowConfigurations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CreatorCanChangeStatus",
                table: "TicketFlowConfigurations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedUserCanChangeStatus",
                table: "TicketFlowConfigurations");

            migrationBuilder.DropColumn(
                name: "CreatorCanChangeStatus",
                table: "TicketFlowConfigurations");
        }
    }
}
