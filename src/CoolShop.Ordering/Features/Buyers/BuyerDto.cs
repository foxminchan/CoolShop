namespace CoolShop.Ordering.Features.Buyers;

public sealed record BuyerDto(
    Guid Id,
    string? Name,
    string? Email,
    string? PhoneNumber,
    string? Address);
