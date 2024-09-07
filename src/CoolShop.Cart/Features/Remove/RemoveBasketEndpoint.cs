namespace CoolShop.Cart.Features.Remove;

public sealed class RemoveBasketEndpoint : IEndpoint<NoContent, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets", async (ISender sender) => await HandleAsync(sender))
            .Produces<NoContent>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Basket))
            .WithName("Delete Basket")
            .WithDescription("Buyer can delete his/her basket")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<NoContent> HandleAsync(ISender request, CancellationToken cancellationToken = default)
    {
        await request.Send(new RemoveBasketCommand(), cancellationToken);

        return TypedResults.NoContent();
    }
}
