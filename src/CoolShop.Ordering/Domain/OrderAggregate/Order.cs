using CoolShop.Ordering.Domain.BuyerAggregate;

namespace CoolShop.Ordering.Domain.OrderAggregate;

public sealed class Order : EntityBase, IAggregateRoot, ISoftDelete
{
    private readonly List<OrderItem> _orderItems = [];

    private Order()
    {
        // EF Core
    }

    public Order(string? note, PaymentMethod paymentMethod, Guid buyerId)
    {
        Note = note;
        Status = Status.Pending;
        PaymentMethod = Guard.Against.EnumOutOfRange(paymentMethod);
        BuyerId = Guard.Against.Default(buyerId);
    }

    public string? Note { get; private set; }
    public Status Status { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public Guid BuyerId { get; private set; }
    public Buyer? Buyer { get; private set; } = default!;

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public decimal TotalPrice => _orderItems.Sum(x => x.Price * x.Quantity);
    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
    }

    public void AddOrderItem(Guid productId, int quantity, decimal price)
    {
        _orderItems.Add(new(productId, quantity, price));
    }

    public void MarkAsShipped()
    {
        Status = Status.Shipped;
    }

    public void MarkAsCancelled()
    {
        Status = Status.Cancelled;
    }

    public void MarkAsHeld()
    {
        Status = Status.Held;
    }

    public void MarkAsRefund()
    {
        Status = Status.Refund;
    }
}
