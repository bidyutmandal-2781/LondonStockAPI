using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stocks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Broker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockStats",
                columns: table => new
                {
                    Ticker = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    TradeCount = table.Column<long>(type: "bigint", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    LastUpdatedUtc = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockStats", x => x.Ticker);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Shares = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    BrokerId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TradeTimeUtc = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrokerStockInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrokerId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    TotalStocks = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AvailableStocks = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerStockInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerStockInfos_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Broker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrokerStockInfos_BrokerId_Ticker",
                table: "BrokerStockInfos",
                columns: new[] { "BrokerId", "Ticker" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trades_Ticker",
                table: "Trades",
                column: "Ticker");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_TradeTimeUtc",
                table: "Trades",
                column: "TradeTimeUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrokerStockInfos");

            migrationBuilder.DropTable(
                name: "StockStats");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Broker");
        }
    }
}
