using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroShopApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntroducingDeliveryAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                comment: "Delivery address for the order");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 22, 12, 23, 8, 147, DateTimeKind.Local).AddTicks(4751));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 26, 12, 23, 8, 147, DateTimeKind.Local).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 16, 12, 23, 8, 147, DateTimeKind.Local).AddTicks(4820));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 31, 12, 23, 8, 147, DateTimeKind.Local).AddTicks(4825));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 29, 12, 23, 8, 147, DateTimeKind.Local).AddTicks(4829));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "AddedOn",
                value: new DateTime(2025, 8, 3, 12, 23, 8, 147, DateTimeKind.Local).AddTicks(4835));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 17, 21, 33, 37, 681, DateTimeKind.Utc).AddTicks(4295));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 21, 21, 33, 37, 681, DateTimeKind.Utc).AddTicks(4306));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 11, 21, 33, 37, 681, DateTimeKind.Utc).AddTicks(4310));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 26, 21, 33, 37, 681, DateTimeKind.Utc).AddTicks(4313));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 24, 21, 33, 37, 681, DateTimeKind.Utc).AddTicks(4315));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 29, 21, 33, 37, 681, DateTimeKind.Utc).AddTicks(4319));
        }
    }
}
