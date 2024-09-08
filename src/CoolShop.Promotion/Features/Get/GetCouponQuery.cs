namespace CoolShop.Promotion.Features.Get;

public sealed record GetCouponQuery(ObjectId Id) : IQuery<Result<CouponDto>>;

public sealed class GetCouponHandler(IPromotionRepository repository)
    : IQueryHandler<GetCouponQuery, Result<CouponDto>>
{
    public async Task<Result<CouponDto>> Handle(GetCouponQuery query, CancellationToken cancellationToken)
    {
        var filter = Builders<Coupon>.Filter.Eq(x => x.Id, query.Id);
        var coupon = await repository.GetAsync(filter, cancellationToken);

        if (coupon is null)
        {
            return Result.NotFound();
        }

        return coupon.ToDto();
    }
}
