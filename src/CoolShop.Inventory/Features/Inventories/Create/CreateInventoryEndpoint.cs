namespace CoolShop.Inventory.Features.Inventories.Create;

public sealed record CreateInventoryRequest(
    int QuantityAvailable,
    int QuantityOnHold,
    Guid? SupplierId,
    Guid? WarehouseId);

public sealed class CreateInventoryEndpoint : IEndpoint<Created<Guid>, CreateInventoryRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/inventories",
                async (CreateInventoryRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Inventory))
            .WithName("Create Inventory")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Created<Guid>> HandleAsync(CreateInventoryRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateInventoryCommand(request.QuantityAvailable, request.QuantityOnHold, request.SupplierId,
            request.WarehouseId);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/inventories/{result.Value}", result.Value);
    }
}
