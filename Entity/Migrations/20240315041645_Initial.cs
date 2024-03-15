using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyOrders",
                columns: table => new
                {
                    BuyOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyOrders", x => x.BuyOrderID);
                });

            migrationBuilder.CreateTable(
                name: "SellOrders",
                columns: table => new
                {
                    SellOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrders", x => x.SellOrderID);
                });

            migrationBuilder.InsertData(
                table: "BuyOrders",
                columns: new[] { "BuyOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[,]
                {
                    { new Guid("91d43927-f0db-42d1-b29e-665d191ef14f"), new DateTime(2024, 3, 15, 11, 45, 0, 0, DateTimeKind.Unspecified), 289.10000000000002, 100L, "Microsoft Corporation", "MSFT" },
                    { new Guid("bfbd9c84-509d-4d43-ba5e-48e5fd6e76b4"), new DateTime(2024, 3, 15, 13, 15, 0, 0, DateTimeKind.Unspecified), 2650.5, 25L, "Alphabet Inc.", "GOOGL" },
                    { new Guid("ec26e4cb-0dfb-4d88-88e2-4b540d4080c9"), new DateTime(2024, 3, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), 152.75, 50L, "Apple Inc.", "AAPL" }
                });

            migrationBuilder.InsertData(
                table: "SellOrders",
                columns: new[] { "SellOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[,]
                {
                    { new Guid("4a6f9c2d-1ef4-4964-9f90-3e19ee55e68d"), new DateTime(2024, 3, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 3500.25, 75L, "Amazon.com Inc.", "AMZN" },
                    { new Guid("d75e6b25-b334-4f20-899d-7f8cb91e489c"), new DateTime(2024, 3, 15, 12, 30, 0, 0, DateTimeKind.Unspecified), 900.5, 50L, "Tesla Inc.", "TSLA" },
                    { new Guid("f381d1f7-69cb-437b-9a39-53c778f98e8d"), new DateTime(2024, 3, 15, 15, 45, 0, 0, DateTimeKind.Unspecified), 575.75, 100L, "NVIDIA Corporation", "NVDA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyOrders");

            migrationBuilder.DropTable(
                name: "SellOrders");
        }
    }
}
