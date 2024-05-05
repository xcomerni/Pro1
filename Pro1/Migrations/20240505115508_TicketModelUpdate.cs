using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pro1.Migrations
{
    /// <inheritdoc />
    public partial class TicketModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Ticket",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePaid",
                table: "Ticket",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "PricePaid",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Ticket");
        }
    }
}
