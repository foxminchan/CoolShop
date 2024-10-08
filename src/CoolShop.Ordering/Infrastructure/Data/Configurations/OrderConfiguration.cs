﻿using CoolShop.Ordering.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Ordering.Infrastructure.Data.Configurations;

internal sealed class OrderConfiguration : BaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.BuyerId)
            .IsRequired();

        builder.Property(e => e.Note)
            .HasMaxLength(DataSchemaLength.Max);

        builder.HasMany(e => e.OrderItems)
            .WithOne()
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(x => x.OrderItems)
            .AutoInclude();

        builder.Navigation(x => x.Buyer)
            .AutoInclude();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
