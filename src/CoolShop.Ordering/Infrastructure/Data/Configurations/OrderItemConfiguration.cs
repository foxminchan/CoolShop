using CoolShop.Ordering.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolShop.Ordering.Infrastructure.Data.Configurations;

internal sealed class OrderItemConfiguration : BaseConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Quantity)
            .IsRequired();

        builder.Property(e => e.Price)
            .IsRequired();

        builder.Property(e => e.OrderId)
            .IsRequired();

        builder.Property(e => e.ProductId)
            .IsRequired();
    }
}
