using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pro1.Migrations
{
    /// <inheritdoc />
    public partial class foreignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Employee_EmployeeId",
                table: "Login");

            migrationBuilder.DropIndex(
                name: "IX_Login_EmployeeId",
                table: "Login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Login_EmployeeId",
                table: "Login",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Employee_EmployeeId",
                table: "Login",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
