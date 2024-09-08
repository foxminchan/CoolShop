namespace CoolShop.Promotion.Features.List;

public sealed class ListCouponsEndpoint : IEndpoint<Ok<List<CouponDto>>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/coupons",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<List<CouponDto>>>()
            .WithTags(nameof(Coupon))
            .WithName("List Coupons")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<List<CouponDto>>> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new ListCouponsQuery(id), cancellationToken);

        return TypedResults.Ok(result.Value.ToList());
    }
}
