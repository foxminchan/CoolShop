using CoolShop.Inventory.Domain.WarehouseAggregator;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Inventory.Infrastructure.Data.Configurations;

internal sealed class WarehouseConfiguration : BaseConfiguration<Warehouse>
{
    public override void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .HasMaxLength(DataSchemaLength.Large)
            .IsRequired();

        builder.Property(e => e.Location)
            .HasMaxLength(DataSchemaLength.SuperLarge);

        builder.HasIndex(e => e.Name)
            .IsUnique();
    }
}
