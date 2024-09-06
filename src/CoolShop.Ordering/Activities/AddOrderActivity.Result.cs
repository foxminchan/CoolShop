namespace CoolShop.Ordering.Activities;

public sealed record AddOrderActivityResult(Guid OrderId, string? BuyerEmail, bool IsSuccess);
