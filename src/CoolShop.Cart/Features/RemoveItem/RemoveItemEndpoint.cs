namespace CoolShop.Cart.Features.RemoveItem;

public sealed class RemoveItemEndpoint : IEndpoint<NoContent, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{productId:guid}",
                async (Guid productId, ISender sender) => await HandleAsync(productId, sender))
            .Produces<NoContent>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Basket))
            .WithName("Remove Item")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<NoContent> HandleAsync(Guid productId, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new RemoveItemCommand(productId), cancellationToken);

        return TypedResults.NoContent();
    }
}
