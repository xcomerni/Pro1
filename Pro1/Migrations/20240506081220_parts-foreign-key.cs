using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pro1.Migrations
{
    /// <inheritdoc />
    public partial class partsforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePrice",
                table: "Ticket",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_TicketId",
                table: "Parts",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Ticket_TicketId",
                table: "Parts",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Ticket_TicketId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_TicketId",
                table: "Parts");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePrice",
                table: "Ticket",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
