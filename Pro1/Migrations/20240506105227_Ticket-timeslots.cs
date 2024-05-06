using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pro1.Migrations
{
    /// <inheritdoc />
    public partial class Tickettimeslots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSlots",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSlots",
                table: "Ticket");
        }
    }
}
