namespace CoolShop.Promotion.Features;

public static class EntityToDto
{
    public static CouponDto ToDto(this Coupon coupon)
    {
        return new(
            coupon.Id,
            coupon.Discount,
            coupon.ValidFrom,
            coupon.ValidTo,
            coupon.Code,
            coupon.ProductIds);
    }

    public static List<CouponDto> ToDtos(this IEnumerable<Coupon> coupons)
    {
        return coupons.Select(ToDto).ToList();
    }
}
