﻿using CoolShop.Ordering.Domain.BuyerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoolShop.Ordering.Infrastructure.Data.Configurations;

internal sealed class BuyerConfiguration : BaseConfiguration<Buyer>
{
    public override void Configure(EntityTypeBuilder<Buyer> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(DataSchemaLength.Large);

        builder.OwnsOne(p => p.Address, e =>
        {
            e.WithOwner();

            e.Property(c => c.Street)
                .HasMaxLength(DataSchemaLength.Medium);

            e.Property(c => c.City)
                .HasMaxLength(DataSchemaLength.Medium);

            e.Property(c => c.Province)
                .HasMaxLength(DataSchemaLength.Medium);
        }).UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.HasMany(e => e.Orders)
            .WithOne(e => e.Buyer)
            .HasForeignKey(e => e.BuyerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(x => x.Orders)
            .AutoInclude();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
