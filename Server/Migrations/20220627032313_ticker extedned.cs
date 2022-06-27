using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blazor_project.Server.Migrations
{
    public partial class tickerextedned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Ticker",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Ticker",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sic",
                table: "Ticker",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Ticker");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Ticker");

            migrationBuilder.DropColumn(
                name: "Sic",
                table: "Ticker");
        }
    }
}
