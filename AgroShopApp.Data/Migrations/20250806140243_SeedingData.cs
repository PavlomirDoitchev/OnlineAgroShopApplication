using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgroShopApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Category identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "Product category in the catalog");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Cart identifier"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User who owns the cart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "User's Cart in the system");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Order identifier"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User who placed the order"),
                    OrderedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the order was placed"),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Current status of the order"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Total amount of the order at the time of purchase"),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "Delivery address for the order")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Submitted order with snapshot data");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Product identifier"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Product name"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Detailed description of the product"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price per unit"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "Optional product image URL"),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date product was added to the store"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Soft-delete timestamp"),
                    StockQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 10, comment: "Available stock quantity"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Whether product is marked as currently available"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Whether product is soft-deleted"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to category")
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
                },
                comment: "Product available for purchase");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Cart identifier (composite key)"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Product added to the cart (composite key)"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Number of items added")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => new { x.CartId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Product-to-cart mapping with quantity");

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User who favorited the product"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Favorited product ID"),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Timestamp when product was added to favorites")
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
                },
                comment: "User-product favorites");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Parent order ID (composite key)"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Ordered product ID (composite key)"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of product in the order"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Unit price of product at the time of purchase")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Mapping between product and order with quantity and price snapshot");

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
                columns: new[] { "Id", "AddedOn", "CategoryId", "DeletedOn", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2025, 7, 23, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7790), 1, null, "Rich, juicy tomatoes perfect for home gardening. Non-GMO and high germination rate.", "/images/seeds-tomato.jpg", true, "Heirloom Tomato Seeds", 3.49m, 100 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2025, 7, 27, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7854), 1, null, "Fast-growing leafy greens ideal for spring gardens.", "/images/seeds-lettuce.jpg", true, "Organic Lettuce Seeds", 2.99m, 80 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2025, 7, 30, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7859), 1, null, "Crunchy cucumbers, suitable for pickling or fresh eating.", "/images/seeds-cucumber.jpg", true, "Cucumber Seeds", 2.59m, 70 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(2025, 7, 25, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7863), 1, null, "Sweet and crisp carrots with fast growth cycle.", "/images/seeds-carrot.jpg", true, "Carrot Seeds", 1.99m, 90 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(2025, 7, 29, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7866), 1, null, "Cold-hardy and packed with nutrition.", "/images/seeds-spinach.jpg", true, "Spinach Seeds", 2.49m, 60 },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new DateTime(2025, 7, 28, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7872), 1, null, "Hot and sweet varieties perfect for salsa.", "/images/seeds-pepper.jpg", true, "Pepper Seeds", 3.99m, 75 },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7881), 1, null, "Aromatic herbs for cooking and companion planting.", "/images/seeds-basil.jpg", true, "Basil Seeds", 1.89m, 100 },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new DateTime(2025, 8, 1, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7884), 1, null, "High-yielding summer squash variety.", "/images/seeds-zucchini.jpg", true, "Zucchini Seeds", 2.39m, 55 },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new DateTime(2025, 7, 26, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7888), 1, null, "Large pumpkins ideal for decoration and pie.", "/images/seeds-pumpkin.jpg", true, "Pumpkin Seeds", 3.25m, 40 },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new DateTime(2025, 8, 2, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7894), 1, null, "Fast-growing root vegetable for spring or fall.", "/images/seeds-radish.jpg", true, "Radish Seeds", 1.79m, 85 },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2025, 7, 17, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7897), 2, null, "All-natural fertilizer for vegetables and flowers.", "/images/fertilizer-organic.jpg", true, "Organic Fertilizer 5kg", 12.95m, 50 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2025, 8, 1, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7931), 2, null, "Concentrated enhancer for better root development.", "/images/fertilizer-liquid.jpg", true, "Liquid Plant Booster", 8.49m, 65 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new DateTime(2025, 7, 24, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7936), 2, null, "Rich compost to improve soil structure.", "/images/fertilizer-compost.jpg", true, "Compost Mix", 6.99m, 70 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new DateTime(2025, 7, 27, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7940), 2, null, "Natural soil amendment high in nutrients.", "/images/fertilizer-worm.jpg", true, "Worm Castings", 9.49m, 45 },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new DateTime(2025, 7, 29, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7945), 2, null, "Promotes strong root growth and blooms.", "/images/fertilizer-bone.jpg", true, "Bone Meal Fertilizer", 5.95m, 60 },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7949), 2, null, "Liquid organic fertilizer for leafy greens.", "/images/fertilizer-fish.jpg", true, "Fish Emulsion", 7.25m, 40 },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new DateTime(2025, 7, 28, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7953), 2, null, "Boosts plant resistance and nutrient uptake.", "/images/fertilizer-seaweed.jpg", true, "Seaweed Extract", 6.75m, 55 },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new DateTime(2025, 8, 2, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7958), 2, null, "Feeds plants for up to 3 months.", "/images/fertilizer-pellets.jpg", true, "Slow Release Pellets", 10.00m, 50 },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new DateTime(2025, 8, 3, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7962), 2, null, "Fast-acting nitrogen formula for leafy growth.", "/images/fertilizer-nitrogen.jpg", true, "Nitrogen Boost", 4.95m, 65 },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new DateTime(2025, 8, 4, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7966), 2, null, "Balanced nutrients for all plants.", "/images/fertilizer-allpurpose.jpg", true, "All-Purpose Fertilizer", 6.49m, 85 },
                    { new Guid("30000000-0000-0000-0000-000000000001"), new DateTime(2025, 7, 30, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7970), 3, null, "Protect your crops from pests without chemicals.", "/images/pesticide-eco.jpg", true, "Eco-Friendly Insect Repellent", 5.75m, 70 },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new DateTime(2025, 8, 4, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7976), 3, null, "Effective against mites, aphids, and fungi.", "/images/pesticide-neem.jpg", true, "Neem Oil Spray", 9.99m, 45 },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new DateTime(2025, 8, 3, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7981), 3, null, "Natural garlic-based spray for flying insects.", "/images/pesticide-garlic.jpg", true, "Garlic Pest Repellent", 4.99m, 60 },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7986), 3, null, "Kills soft-bodied insects on contact.", "/images/pesticide-soap.jpg", true, "Insecticidal Soap", 6.50m, 50 },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new DateTime(2025, 7, 29, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7990), 3, null, "Fast knockdown effect for garden pests.", "/images/pesticide-pyrethrin.jpg", true, "Pyrethrin Spray", 8.75m, 55 },
                    { new Guid("30000000-0000-0000-0000-000000000006"), new DateTime(2025, 8, 1, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7994), 3, null, "Effective for snails and slugs in vegetable beds.", "/images/pesticide-slug.jpg", true, "Slugo Bait", 5.25m, 40 },
                    { new Guid("30000000-0000-0000-0000-000000000007"), new DateTime(2025, 8, 2, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7998), 3, null, "Bacillus thuringiensis for caterpillar management.", "/images/pesticide-bt.jpg", true, "BT Caterpillar Control", 7.85m, 45 },
                    { new Guid("30000000-0000-0000-0000-000000000008"), new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(8001), 3, null, "Diatomaceous earth for crawling insects.", "/images/pesticide-de.jpg", true, "DE Powder", 6.10m, 35 },
                    { new Guid("30000000-0000-0000-0000-000000000009"), new DateTime(2025, 7, 28, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(8004), 3, null, "Repels rodents and bugs naturally.", "/images/pesticide-chili.jpg", true, "Chili Pepper Spray", 3.99m, 50 },
                    { new Guid("30000000-0000-0000-0000-000000000010"), new DateTime(2025, 7, 27, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(8007), 3, null, "Fungicide and mite control for vegetables.", "/images/pesticide-sulfur.jpg", true, "Sulfur Dust", 4.25m, 60 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
