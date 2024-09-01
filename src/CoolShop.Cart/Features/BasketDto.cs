namespace CoolShop.Cart.Features;

public sealed record BasketDto(
    string Id,
    Guid? CouponId,
    List<BasketItemDto> Items,
    decimal TotalPrice);

public sealed record BasketItemDto(
    Guid Id,
    string Name,
    int Quantity,
    decimal Price,
    decimal PriceSale);
