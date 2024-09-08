namespace CoolShop.Promotion.Features.Get;

public sealed class GetCouponEndpoint : IEndpoint<Ok<CouponDto>, ObjectId, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/coupons/{id}",
                async (ObjectId id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<CouponDto>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Coupon))
            .WithName("Get Coupon")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<CouponDto>> HandleAsync(ObjectId id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetCouponQuery(id), cancellationToken);

        return TypedResults.Ok(result.Value);
    }
}
