using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Activities;

public sealed record AddOrderActivityData(Guid BuyerId, string? Note, PaymentMethod PaymentMethod, List<Item> Items);

public sealed record Item(Guid ProductId, int Quantity, decimal Price);
