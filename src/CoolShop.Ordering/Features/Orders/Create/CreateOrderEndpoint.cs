using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Features.Orders.Create;

public sealed record CreateOrderRequest(string? Note, PaymentMethod PaymentMethod);

public sealed class CreateOrderEndpoint : IEndpoint<Created<Guid>, CreateOrderRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders",
                async (
                    [FromIdempotencyHeader] string key,
                    CreateOrderRequest request, ISender sender) => await HandleAsync(request, sender))
            .AddEndpointFilter<IdempotencyFilter>()
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Order))
            .WithName("Create Order")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Created<Guid>> HandleAsync(CreateOrderRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateOrderCommand(request.Note, request.PaymentMethod);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/{result.Value}", result.Value);
    }
}
