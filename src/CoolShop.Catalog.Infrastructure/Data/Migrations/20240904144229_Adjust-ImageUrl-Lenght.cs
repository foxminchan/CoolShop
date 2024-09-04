using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolShop.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustImageUrlLenght : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(4385),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2391));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(4162),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2154));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(2638),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(375));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(2497),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(695),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(471),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7723));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2391),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(4385));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2154),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(375),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(2638));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(156),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(2497));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7981),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(695));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7723),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 42, 28, 980, DateTimeKind.Utc).AddTicks(471));
        }
    }
}
