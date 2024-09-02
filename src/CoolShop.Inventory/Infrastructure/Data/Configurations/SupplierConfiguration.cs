using CoolShop.Inventory.Domain.SupplierAggregator;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Inventory.Infrastructure.Data.Configurations;

internal sealed class SupplierConfiguration : BaseConfiguration<Supplier>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .HasMaxLength(DataSchemaLength.Large)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(DataSchemaLength.Tiny);

        builder.HasIndex(e => e.Name)
            .IsUnique();
    }
}
