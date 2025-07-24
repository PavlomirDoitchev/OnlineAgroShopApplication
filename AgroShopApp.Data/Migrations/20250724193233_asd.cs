using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgroShopApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 10),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Seeds" },
                    { 2, "Fertilizer" },
                    { 3, "Pesticide" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 7, 10, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9204), 1, "Rich, juicy tomatoes perfect for home gardening. Non-GMO and high germination rate.", "/images/seeds-tomato.jpg", true, "Heirloom Tomato Seeds", 3.49m, 100 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 7, 14, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9214), 1, "Fast-growing leafy greens ideal for spring gardens.", "/images/seeds-lettuce.jpg", true, "Organic Lettuce Seeds", 2.99m, 80 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 7, 4, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9216), 2, "Boost your plant health with organic nutrients. Safe for vegetables and flowers.", "/images/fertilizer-organic.jpg", true, "All-Natural Fertilizer 5kg", 12.95m, 50 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 7, 19, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9219), 2, "Concentrated growth enhancer for root development and yield.", "/images/fertilizer-liquid.jpg", true, "Liquid Plant Booster", 8.49m, 65 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 7, 17, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9228), 3, "Protect your crops from pests without harmful chemicals.", "/images/pesticide-eco.jpg", true, "Eco-Friendly Insect Repellent", 5.75m, 70 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 7, 22, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9232), 3, "Effective natural solution against leaf-eating insects.", "/images/pesticide-neem.jpg", true, "Neem Oil Pesticide 1L", 9.99m, 45 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
