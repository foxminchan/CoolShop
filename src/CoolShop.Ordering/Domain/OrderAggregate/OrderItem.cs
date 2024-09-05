namespace CoolShop.Ordering.Domain.OrderAggregate;

public sealed class OrderItem : EntityBase
{
    private OrderItem()
    {
        // EF Core
    }

    public OrderItem(Guid productId, int quantity, decimal price)
    {
        ProductId = Guard.Against.Default(productId);
        Quantity = Guard.Against.NegativeOrZero(quantity);
        Price = Guard.Against.Negative(price, nameof(price));
    }

    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid OrderId { get; }
}
