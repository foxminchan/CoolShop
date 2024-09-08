namespace CoolShop.Promotion.Features.List;

public sealed record ListCouponsQuery(Guid ProductId) : IQuery<Result<IEnumerable<CouponDto>>>;

public sealed class ListCouponsHandler(IPromotionRepository repository)
    : IQueryHandler<ListCouponsQuery, Result<IEnumerable<CouponDto>>>
{
    public async Task<Result<IEnumerable<CouponDto>>> Handle(ListCouponsQuery request,
        CancellationToken cancellationToken)
    {
        var coupons = await repository.ListAsync(request.ProductId, cancellationToken);

        return coupons.ToDtos();
    }
}
