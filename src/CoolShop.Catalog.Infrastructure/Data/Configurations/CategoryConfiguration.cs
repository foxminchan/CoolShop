using CoolShop.Catalog.Domain;
using CoolShop.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Catalog.Infrastructure.Data.Configurations;

internal sealed class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
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
