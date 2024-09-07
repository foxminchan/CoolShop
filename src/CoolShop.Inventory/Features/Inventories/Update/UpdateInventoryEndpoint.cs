namespace CoolShop.Inventory.Features.Inventories.Update;

public sealed record UpdateInventoryRequest(Guid Id, int QuantityOnHold, Guid? SupplierId, Guid? WarehouseId);

public sealed class UpdateInventoryEndpoint : IEndpoint<Ok, UpdateInventoryRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/inventories",
                async (UpdateInventoryRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Inventory))
            .WithName("Update Inventory")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Ok> HandleAsync(UpdateInventoryRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command =
            new UpdateInventoryCommand(request.Id, request.QuantityOnHold, request.SupplierId, request.WarehouseId);

        await sender.Send(command, cancellationToken);

        return TypedResults.Ok();
    }
}
