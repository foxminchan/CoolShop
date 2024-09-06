using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Features.Orders.Ship;

public sealed class ShipOrderEndpoint : IEndpoint<Ok, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/orders/{id:guid}/ship",
                async ([FromIdempotencyHeader] string key, Guid id, ISender sender) => await HandleAsync(id, sender))
            .AddEndpointFilter<IdempotencyFilter>()
            .Produces<Ok>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Order))
            .WithName("Ship Order")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok> HandleAsync(Guid id, ISender sender, CancellationToken cancellationToken = default)
    {
        await sender.Send(new ShipOrderCommand(id), cancellationToken);

        return TypedResults.Ok();
    }
}
