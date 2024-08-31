using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolShop.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifySoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7712),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 756, DateTimeKind.Utc).AddTicks(5462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7289),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 756, DateTimeKind.Utc).AddTicks(4962));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7578),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 755, DateTimeKind.Utc).AddTicks(23));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7220),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 754, DateTimeKind.Utc).AddTicks(9483));

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 561, DateTimeKind.Utc).AddTicks(1292),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 751, DateTimeKind.Utc).AddTicks(4327));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 560, DateTimeKind.Utc).AddTicks(2931),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 750, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "brands",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "brands");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 756, DateTimeKind.Utc).AddTicks(5462),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7712));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 756, DateTimeKind.Utc).AddTicks(4962),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 564, DateTimeKind.Utc).AddTicks(7289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 755, DateTimeKind.Utc).AddTicks(23),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7578));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 754, DateTimeKind.Utc).AddTicks(9483),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 563, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 751, DateTimeKind.Utc).AddTicks(4327),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 561, DateTimeKind.Utc).AddTicks(1292));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 8, 56, 45, 750, DateTimeKind.Utc).AddTicks(590),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 30, 15, 57, 27, 560, DateTimeKind.Utc).AddTicks(2931));
        }
    }
}
