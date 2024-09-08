namespace CoolShop.Promotion.Features.Delete;

public sealed class DeleteCouponEndpoint : IEndpoint<NoContent, ObjectId, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/coupons/{id}",
                async (ObjectId id, ISender sender) => await HandleAsync(id, sender))
            .Produces<NoContent>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Coupon))
            .WithName("Delete Coupon")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<NoContent> HandleAsync(ObjectId id, ISender sender, CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteCouponCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}
