namespace CoolShop.Promotion.Features.Create;

public sealed record CreateCouponRequest(
    decimal Discount,
    DateOnly ValidFrom,
    DateOnly ValidTo,
    string Code,
    Guid[]? ProductIds);

public sealed class CreateCouponEndpoint : IEndpoint<Created<ObjectId>, CreateCouponRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/coupons",
                async (CreateCouponRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<ObjectId>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Coupon))
            .WithName("Create Coupon")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Created<ObjectId>> HandleAsync(CreateCouponRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCouponCommand(request.Discount, request.ValidFrom, request.ValidTo, request.Code,
            request.ProductIds);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/coupons/{result.Value}", result.Value);
    }
}
