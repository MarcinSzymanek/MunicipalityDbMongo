using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class ReplacedIdWithCPR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "CPRNumber");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reservations",
                newName: "UserCPRNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                newName: "IX_Reservations_UserCPRNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserCPRNumber",
                table: "Reservations",
                column: "UserCPRNumber",
                principalTable: "Users",
                principalColumn: "CPRNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserCPRNumber",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "CPRNumber",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserCPRNumber",
                table: "Reservations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserCPRNumber",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
