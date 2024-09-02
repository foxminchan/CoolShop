using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Create;

public sealed record CreateWarehouseRequest(string? Name, string? Location, int Capacity);

public sealed class CreateWarehouseEndpoint : IEndpoint<Created<Guid>, CreateWarehouseRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/warehouses",
                async (CreateWarehouseRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Warehouse))
            .WithName("Create Warehouse")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Created<Guid>> HandleAsync(CreateWarehouseRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateWarehouseCommand(request.Name, request.Location, request.Capacity);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/{result.Value}", result.Value);
    }
}
