namespace CoolShop.Cart.Features.ReduceItemQuantity;

public sealed record ReduceItemQuantityRequest(Guid ProductId, int Quantity);

public sealed class ReduceItemQuantityEndpoint : IEndpoint<Ok, ReduceItemQuantityRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/baskets/reduce",
                async (ReduceItemQuantityRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Basket))
            .WithName("Reduce Item Quantity")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Ok> HandleAsync(ReduceItemQuantityRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new ReduceItemQuantityCommand(request.ProductId, request.Quantity), cancellationToken);

        return TypedResults.Ok();
    }
}
