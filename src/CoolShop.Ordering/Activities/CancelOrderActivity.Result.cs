namespace CoolShop.Ordering.Activities;

public sealed record CancelOrderActivityResult(bool IsSuccess, string? BuyerEmail);
