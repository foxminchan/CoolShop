namespace CoolShop.Ordering.Workflows;

public sealed record CreateOrderWorkflowResult(Guid OrderId, bool IsSuccess);
