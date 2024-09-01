using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Catalog.Infrastructure.Data.Configurations;

internal sealed class BrandConfiguration : BaseConfiguration<Brand>
{
    public override void Configure(EntityTypeBuilder<Brand> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(DataSchemaLength.Medium)
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
