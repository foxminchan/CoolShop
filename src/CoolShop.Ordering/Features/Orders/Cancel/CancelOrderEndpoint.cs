using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Features.Orders.Cancel;

public sealed class CancelOrderEndpoint : IEndpoint<Ok, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{id:guid}/cancel",
                async ([FromIdempotencyHeader] string key, Guid id, ISender sender) => await HandleAsync(id, sender))
            .AddEndpointFilter<IdempotencyFilter>()
            .Produces<Ok>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Order))
            .WithName("Cancel Order")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Ok> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CancelOrderCommand(id);

        await sender.Send(command, cancellationToken);

        return TypedResults.Ok();
    }
}
