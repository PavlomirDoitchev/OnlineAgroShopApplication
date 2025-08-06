using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroShopApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 23, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9677));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 27, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 30, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9795), "https://images.unsplash.com/photo-1449300079323-02e209d9d3a6?q=80&w=1548&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 25, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9799), "https://images.unsplash.com/photo-1447175008436-054170c2e979?q=80&w=1998&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 29, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9802), "https://images.unsplash.com/photo-1576045057995-568f588f82fb?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 28, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9807), "https://images.unsplash.com/photo-1608737637507-9aaeb9f4bf30?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9811), "https://images.unsplash.com/photo-1627738663093-d0779d56e3bc?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 1, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9826), "https://images.unsplash.com/photo-1596056094719-10ba4f7ea650?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 26, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9830), "https://images.unsplash.com/photo-1506917728037-b6af01a7d403?q=80&w=1548&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 2, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9837), "https://images.unsplash.com/photo-1589753014594-0676c69bbcbe?q=80&w=1480&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 17, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "AddedOn",
                value: new DateTime(2025, 8, 1, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9844));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 24, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9847), "https://images.unsplash.com/photo-1649577193391-f13d769d011d?q=80&w=1742&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 27, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9850), "https://unclejimswormfarm.com/wp-content/uploads/2016/02/harvesting-worm-castings.jpeg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 29, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9855), "https://radhakrishnaagriculture.in/cdn/shop/files/boneMeal.jpg?v=1711435429" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9860), "https://www.pennington.com/-/media/Project/OneWeb/Pennington/Images/blog/fertilizer/what-is-fish-fertilizer/fish-fertilizer-og.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 28, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9864), "https://www.marketresearchintellect.com/images/blogs/ocean-s-bounty-the-rising-tide-of-seaweed-fertilizer-market-growth.webp" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000008"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 2, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9868), "https://assets.manufactum.de/p/207/207508/207508_02.jpg/organic-fertilizer-sheep-wool-pellets.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000009"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 3, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9872), "https://cdn.shopify.com/s/files/1/0015/4976/2632/files/Nitrogen_Max1_whiite.jpg?v=1752089739&width=600&height=600&crop=center" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 4, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9913), "https://i5.walmartimages.com/seo/Expert-Gardener-All-Purpose-Plant-Food-Fertilizer-12-0-12-40-lb_b0d92f08-b9c5-4ccd-b212-42087b2ce829.76e288d920602343a57f64968148409e.jpeg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 30, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9917));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "AddedOn",
                value: new DateTime(2025, 8, 4, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9921));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 3, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9924), "https://www.arbico-organics.com/images/uploads/1452603_Garlic_Barrier_AG_600x600.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9929), "https://m.media-amazon.com/images/I/81-uTI1JLnL._UF350,350_QL80_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 29, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9935), "https://m.media-amazon.com/images/I/61Cmdc161eL.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 1, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9938), "https://www.nexles.com/media/catalog/product/cache/18/thumbnail/500x/8083c875e83be300356bb052a4e4af68/a/u/au_190090_def_ps.png.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 2, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9942), "https://files.plytix.com/api/v1.1/file/public_files/pim/assets/43/37/8d/5e/5e8d3743202d9eba64d3af60/images/12/a0/da/63/63daa01245952636f4885023/8066_LifeStyle_02.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000008"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9945), "https://dombikagro.com/image/catalog/HOMEVO/organichen_preparat_pesticiden_neutralizator_homevo_homevo_neutralizator_pesticide.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000009"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 28, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9948), "https://m.media-amazon.com/images/I/612sBQNgyNL.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000010"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 27, 17, 20, 1, 643, DateTimeKind.Local).AddTicks(9953), "https://m.media-amazon.com/images/I/51sTPn6ij7L._SL1500_.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 23, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 27, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7854));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 30, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7859), "/images/seeds-cucumber.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 25, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7863), "/images/seeds-carrot.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 29, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7866), "/images/seeds-spinach.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 28, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7872), "/images/seeds-pepper.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7881), "/images/seeds-basil.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 1, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7884), "/images/seeds-zucchini.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 26, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7888), "/images/seeds-pumpkin.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 2, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7894), "/images/seeds-radish.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 17, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "AddedOn",
                value: new DateTime(2025, 8, 1, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 24, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7936), "/images/fertilizer-compost.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 27, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7940), "/images/fertilizer-worm.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 29, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7945), "/images/fertilizer-bone.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7949), "/images/fertilizer-fish.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 28, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7953), "/images/fertilizer-seaweed.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000008"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 2, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7958), "/images/fertilizer-pellets.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000009"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 3, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7962), "/images/fertilizer-nitrogen.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 4, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7966), "/images/fertilizer-allpurpose.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 30, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "AddedOn",
                value: new DateTime(2025, 8, 4, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7976));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 3, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7981), "/images/pesticide-garlic.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7986), "/images/pesticide-soap.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 29, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7990), "/images/pesticide-pyrethrin.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 1, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7994), "/images/pesticide-slug.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 2, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(7998), "/images/pesticide-bt.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000008"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 31, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(8001), "/images/pesticide-de.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000009"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 28, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(8004), "/images/pesticide-chili.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000010"),
                columns: new[] { "AddedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 27, 17, 2, 42, 835, DateTimeKind.Local).AddTicks(8007), "/images/pesticide-sulfur.jpg" });
        }
    }
}
