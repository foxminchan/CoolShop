using CoolShop.Inventory.Domain.SupplierAggregator;
using CoolShop.Inventory.Domain.WarehouseAggregator;
using Microsoft.EntityFrameworkCore;
using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Infrastructure.Data;

public sealed class InventoryContext(DbContextOptions<InventoryContext> options) : DbContext(options)
{
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<InventoryModel> Inventories => Set<InventoryModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension(UniqueType.Extension);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryContext).Assembly);
    }
}
