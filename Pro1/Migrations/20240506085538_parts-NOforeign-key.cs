using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pro1.Migrations
{
    /// <inheritdoc />
    public partial class partsNOforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Ticket_TicketId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_TicketId",
                table: "Parts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
