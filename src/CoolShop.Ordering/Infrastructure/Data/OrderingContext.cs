using CoolShop.Ordering.Domain.BuyerAggregate;
using CoolShop.Ordering.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace CoolShop.Ordering.Infrastructure.Data;

public sealed class OrderingContext(DbContextOptions<OrderingContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Buyer> Buyers => Set<Buyer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension(UniqueType.Extension);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingContext).Assembly);
    }
}
