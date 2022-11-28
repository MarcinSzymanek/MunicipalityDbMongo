using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class CPRNumber_SET_IDENTITY_ON : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Reservations_Users_UserCPRNumber", "Reservations");
            migrationBuilder.DropTable("Users");
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CPRNumber = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVR = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.CPRNumber);
                });

            migrationBuilder.AddForeignKey("FK_Reservations_Users_UserCPRNumber", "Reservations", "UserCPRNumber", "Users");
        }

        // Can't use this and hard to change, we will not go back
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CPRNumber",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
