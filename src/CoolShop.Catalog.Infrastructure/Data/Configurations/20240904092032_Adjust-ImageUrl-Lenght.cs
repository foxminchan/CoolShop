using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolShop.Catalog.Infrastructure.Data.Configurations
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
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2391),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(5493));

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2154),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(5289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(375),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3899));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(156),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3765));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7981),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(2096));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7723),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(1885));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2391));

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(5289),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(2154));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3899),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(375));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(3765),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 562, DateTimeKind.Utc).AddTicks(156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(2096),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 16, 22, 52, 90, DateTimeKind.Utc).AddTicks(1885),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 9, 20, 31, 561, DateTimeKind.Utc).AddTicks(7723));
        }
    }
}
