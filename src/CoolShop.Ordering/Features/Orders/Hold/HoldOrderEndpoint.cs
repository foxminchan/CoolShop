using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Features.Orders.Hold;

public sealed class HoldOrderEndpoint : IEndpoint<Ok, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/orders/{id:guid}/hold",
                async ([FromIdempotencyHeader] string key, Guid id, ISender sender) => await HandleAsync(id, sender))
            .AddEndpointFilter<IdempotencyFilter>()
            .Produces<Ok>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Order))
            .WithName("Hold Order")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok> HandleAsync(Guid id, ISender sender, CancellationToken cancellationToken = default)
    {
        await sender.Send(new HoldOrderCommand(id), cancellationToken);

        return TypedResults.Ok();
    }
}
