namespace CoolShop.Ordering.Features.Buyers;

public sealed record BuyerDto(
    Guid Id,
    string? Name,
    string? Address);
