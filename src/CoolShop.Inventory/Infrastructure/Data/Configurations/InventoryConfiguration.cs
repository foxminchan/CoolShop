using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Inventory.Infrastructure.Data.Configurations;

internal sealed class InventoryConfiguration : BaseConfiguration<Domain.InventoryAggregator.Inventory>
{
    public override void Configure(EntityTypeBuilder<Domain.InventoryAggregator.Inventory> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.QuantityAvailable)
            .IsRequired();

        builder.Property(e => e.QuantityOnHold)
            .IsRequired();

        builder.HasOne(e => e.Supplier)
            .WithMany()
            .HasForeignKey(e => e.SupplierId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Warehouse)
            .WithMany()
            .HasForeignKey(e => e.WarehouseId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Navigation(e => e.Supplier)
            .AutoInclude();

        builder.Navigation(e => e.Warehouse)
            .AutoInclude();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
