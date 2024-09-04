namespace CoolShop.Ordering.Activities;

public sealed record AddOrderActivityResult(Guid OrderId, bool IsSuccess);
