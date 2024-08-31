using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolShop.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(5493),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7712));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(5289),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7289));

            migrationBuilder.AddColumn<double>(
                name: "rating",
                table: "products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "reviews_count",
                table: "products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3899),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7578));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3765),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(2096),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 561, DateTimeKind.Utc).AddTicks(1292));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(1885),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 560, DateTimeKind.Utc).AddTicks(2931));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rating",
                table: "products");

            migrationBuilder.DropColumn(
                name: "reviews_count",
                table: "products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7712),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(5493));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7289),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(5289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7578),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3899));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7220),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3765));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 561, DateTimeKind.Utc).AddTicks(1292),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(2096));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 560, DateTimeKind.Utc).AddTicks(2931),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(1885));
        }
    }
}
