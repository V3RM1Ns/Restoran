using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 2, "Classic Burger", 12.50m },
                    { 2, 5, "Caesar Salad", 9.00m },
                    { 3, 4, "Coca-Cola", 3.00m },
                    { 4, 7, "Fries", 4.50m },
                    { 5, 3, "Chocolate Lava Cake", 7.50m },
                    { 6, 2, "Spaghetti Bolognese", 15.00m },
                    { 7, 4, "Orange Juice", 4.00m },
                    { 8, 1, "Chicken Wings (6 pcs)", 10.00m },
                    { 9, 3, "Cheesecake", 6.50m },
                    { 10, 2, "Vegetable Pizza", 14.00m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 20.00m },
                    { 2, new DateTime(2025, 7, 3, 14, 30, 0, 0, DateTimeKind.Unspecified), 19.00m },
                    { 3, new DateTime(2025, 7, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), 22.50m },
                    { 4, new DateTime(2025, 7, 7, 11, 45, 0, 0, DateTimeKind.Unspecified), 24.50m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Count", "MenuItemId", "OrderId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 4, 1 },
                    { 3, 1, 3, 1 },
                    { 4, 1, 2, 2 },
                    { 5, 1, 8, 2 },
                    { 6, 1, 6, 3 },
                    { 7, 1, 5, 3 },
                    { 8, 1, 10, 4 },
                    { 9, 1, 7, 4 },
                    { 10, 1, 9, 4 },
                    { 11, 2, 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
