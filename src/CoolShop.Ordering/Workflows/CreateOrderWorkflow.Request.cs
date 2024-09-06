using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Workflows;

public sealed record CreateOrderWorkflowRequest(Guid BuyerId, string? Note, PaymentMethod PaymentMethod);
