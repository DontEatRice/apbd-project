using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blazor_project.Server.Migrations
{
    public partial class watchlistadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ticker",
                columns: table => new
                {
                    IdTicker = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TickerSymbol = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticker", x => x.IdTicker);
                });

            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    IdTicker = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlist", x => new { x.IdTicker, x.IdUser });
                    table.ForeignKey(
                        name: "FK_Watchlist_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Watchlist_Ticker_IdTicker",
                        column: x => x.IdTicker,
                        principalTable: "Ticker",
                        principalColumn: "IdTicker");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Watchlist_IdUser",
                table: "Watchlist",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Watchlist");

            migrationBuilder.DropTable(
                name: "Ticker");
        }
    }
}
