using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroShopApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingDeleteTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AddedOn", "DeletedOn" },
                values: new object[] { new DateTime(2025, 7, 11, 16, 13, 32, 867, DateTimeKind.Utc).AddTicks(7421), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AddedOn", "DeletedOn" },
                values: new object[] { new DateTime(2025, 7, 15, 16, 13, 32, 867, DateTimeKind.Utc).AddTicks(7430), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AddedOn", "DeletedOn" },
                values: new object[] { new DateTime(2025, 7, 5, 16, 13, 32, 867, DateTimeKind.Utc).AddTicks(7434), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "AddedOn", "DeletedOn" },
                values: new object[] { new DateTime(2025, 7, 20, 16, 13, 32, 867, DateTimeKind.Utc).AddTicks(7437), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "AddedOn", "DeletedOn" },
                values: new object[] { new DateTime(2025, 7, 18, 16, 13, 32, 867, DateTimeKind.Utc).AddTicks(7439), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "AddedOn", "DeletedOn" },
                values: new object[] { new DateTime(2025, 7, 23, 16, 13, 32, 867, DateTimeKind.Utc).AddTicks(7452), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 10, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9204));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 14, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9214));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 4, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9216));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 19, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9219));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 17, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9228));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "AddedOn",
                value: new DateTime(2025, 7, 22, 19, 32, 32, 960, DateTimeKind.Utc).AddTicks(9232));
        }
    }
}
