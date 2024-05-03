using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pro1.Migrations
{
    /// <inheritdoc />
    public partial class sixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Login_EmployeeId",
                table: "Login");

            migrationBuilder.CreateIndex(
                name: "IX_Login_EmployeeId",
                table: "Login",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Login_EmployeeId",
                table: "Login");

            migrationBuilder.CreateIndex(
                name: "IX_Login_EmployeeId",
                table: "Login",
                column: "EmployeeId",
                unique: true);
        }
    }
}
