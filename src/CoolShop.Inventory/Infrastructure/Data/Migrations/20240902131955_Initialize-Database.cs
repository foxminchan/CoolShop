using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolShop.Inventory.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 9, 2, 13, 19, 54, 447, DateTimeKind.Utc).AddTicks(8877)),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2024, 9, 2, 13, 19, 54, 447, DateTimeKind.Utc).AddTicks(9056)),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_suppliers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 9, 2, 13, 19, 54, 448, DateTimeKind.Utc).AddTicks(933)),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2024, 9, 2, 13, 19, 54, 448, DateTimeKind.Utc).AddTicks(1132)),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    quantity_available = table.Column<int>(type: "integer", nullable: false),
                    quantity_on_hold = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 9, 2, 13, 19, 54, 447, DateTimeKind.Utc).AddTicks(5349)),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2024, 9, 2, 13, 19, 54, 447, DateTimeKind.Utc).AddTicks(5658)),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventories", x => x.id);
                    table.ForeignKey(
                        name: "fk_inventories_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_inventories_warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventories_supplier_id",
                table: "inventories",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventories_warehouse_id",
                table: "inventories",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "ix_suppliers_name",
                table: "suppliers",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_warehouses_name",
                table: "warehouses",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "warehouses");
        }
    }
}
