using CoolShop.Inventory.Domain.SupplierAggregator;
using CoolShop.Inventory.Domain.WarehouseAggregator;
using Microsoft.EntityFrameworkCore;

namespace CoolShop.Inventory.Infrastructure.Data;

public sealed class InventoryContext(DbContextOptions<InventoryContext> options) : DbContext(options)
{
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Domain.Inventory> Inventories => Set<Domain.Inventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension(UniqueType.Extension);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryContext).Assembly);
    }
}
