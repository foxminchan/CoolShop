using CoolShop.Catalog.Domain.ProductAggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Catalog.Infrastructure.Data.Configurations;

internal class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(DataSchemaLength.Medium)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(DataSchemaLength.SuperLarge);

        builder.OwnsOne(
            p => p.Price,
            e => e.ToJson()
        );

        builder.Property(x => x.Image)
            .HasMaxLength(DataSchemaLength.SuperLarge);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Navigation(e => e.Category)
            .AutoInclude();

        builder.Navigation(e => e.Brand)
            .AutoInclude();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
