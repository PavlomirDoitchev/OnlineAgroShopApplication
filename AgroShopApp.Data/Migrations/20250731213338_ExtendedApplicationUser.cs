using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroShopApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 14, 20, 4, 6, 448, DateTimeKind.Utc).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 18, 20, 4, 6, 448, DateTimeKind.Utc).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 8, 20, 4, 6, 448, DateTimeKind.Utc).AddTicks(7462));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 23, 20, 4, 6, 448, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 21, 20, 4, 6, 448, DateTimeKind.Utc).AddTicks(7467));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 26, 20, 4, 6, 448, DateTimeKind.Utc).AddTicks(7470));
        }
    }
}
