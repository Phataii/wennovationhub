using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wennovation_hub.Migrations
{
    public partial class entityupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Wennovators",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Wennovators",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "Bookings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Wennovators");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Wennovators");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Bookings");
        }
    }
}
