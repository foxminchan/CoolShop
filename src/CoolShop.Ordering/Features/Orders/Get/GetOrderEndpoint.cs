using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Features.Orders.Get;

public sealed class GetOrderEndpoint : IEndpoint<Ok<OrderDetailDto>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<OrderDetailDto>>()
            .WithTags(nameof(Order))
            .WithName("Get Order")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<OrderDetailDto>> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetOrderQuery(id), cancellationToken);

        return TypedResults.Ok(response.Value);
    }
}
