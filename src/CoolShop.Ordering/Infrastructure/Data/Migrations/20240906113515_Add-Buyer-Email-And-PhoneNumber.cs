using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolShop.Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBuyerEmailAndPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(4961),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(4749),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(508));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "order_items",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(9698),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(3206));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "order_items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(9511),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(3031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "buyers",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 189, DateTimeKind.Utc).AddTicks(7795),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 570, DateTimeKind.Utc).AddTicks(4826));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "buyers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 189, DateTimeKind.Utc).AddTicks(7547),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 570, DateTimeKind.Utc).AddTicks(4566));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "buyers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "buyers",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "buyers");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "buyers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(720),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(508),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(4749));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "order_items",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(3206),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(9698));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "order_items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 572, DateTimeKind.Utc).AddTicks(3031),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 192, DateTimeKind.Utc).AddTicks(9511));

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date",
                table: "buyers",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 570, DateTimeKind.Utc).AddTicks(4826),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 189, DateTimeKind.Utc).AddTicks(7795));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "buyers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 14, 43, 10, 570, DateTimeKind.Utc).AddTicks(4566),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 6, 11, 35, 14, 189, DateTimeKind.Utc).AddTicks(7547));
        }
    }
}
