using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class Migration3_GPS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacClosestAdr",
                table: "Facilities");

            migrationBuilder.AddColumn<double>(
                name: "GPS_lat",
                table: "Facilities",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GPS_lon",
                table: "Facilities",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GPS_lat",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "GPS_lon",
                table: "Facilities");

            migrationBuilder.AddColumn<string>(
                name: "FacClosestAdr",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
