using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trading.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketPriceStates",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentBidPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    CurrentAskPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    CurrentMarketPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    CurrentSpread = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    CurrentSpreadPercent = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    CurrentTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PreviousBidPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: true),
                    PreviousAskPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: true),
                    PreviousMarketPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: true),
                    PreviousSpread = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: true),
                    PreviousSpreadPercent = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: true),
                    PreviousTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPriceStates", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Side = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DecisionStatus = table.Column<string>(type: "TEXT", nullable: false),
                    RulesVersion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradingRules",
                columns: table => new
                {
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxNotional = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    MaxQuantity = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    PriceDeviationPercent = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    DuplicateIdPreventionEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    SymbolWhitelistEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    SymbolWhitelistJson = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingRules", x => x.Version);
                });

            migrationBuilder.CreateTable(
                name: "OrderRejectionReasons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderEntityId = table.Column<long>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRejectionReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderRejectionReasons_Orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRejectionReasons_OrderEntityId",
                table: "OrderRejectionReasons",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderId",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Symbol_Timestamp",
                table: "Orders",
                columns: new[] { "Symbol", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketPriceStates");

            migrationBuilder.DropTable(
                name: "OrderRejectionReasons");

            migrationBuilder.DropTable(
                name: "TradingRules");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
