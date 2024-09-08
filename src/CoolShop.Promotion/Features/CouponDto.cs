namespace CoolShop.Promotion.Features;

public sealed record CouponDto(
    ObjectId Id,
    decimal Discount,
    DateOnly ValidFrom,
    DateOnly ValidTo,
    string Code,
    Guid[]? ProductIds);
