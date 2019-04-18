using Microsoft.EntityFrameworkCore.Migrations;

namespace coreBookStore.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Publications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_AdminId",
                table: "Publications",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Admins_AdminId",
                table: "Publications",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Admins_AdminId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_AdminId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Publications");
        }
    }
}
