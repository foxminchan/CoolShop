namespace CoolShop.Cart.Features.Create;

public sealed record CreateBasketRequest(Guid ProductId, Guid CouponId, int Quantity);

public sealed class CreateBasketEndpoint : IEndpoint<Created<string>, CreateBasketRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets",
                async (CreateBasketRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<string>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Basket))
            .WithName("Create Basket")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Created<string>> HandleAsync(CreateBasketRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new CreateBasketCommand(request.ProductId, request.CouponId, request.Quantity),
            cancellationToken);

        return TypedResults.Created($"/api/v1/{result.Value}", result.Value);
    }
}
