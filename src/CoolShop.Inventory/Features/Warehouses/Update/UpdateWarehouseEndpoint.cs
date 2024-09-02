using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Update;

public sealed record UpdateWarehouseRequest(Guid Id, string? Name, string? Location, int Capacity);

public sealed class UpdateWarehouseEndpoint : IEndpoint<Ok, UpdateWarehouseRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/warehouses",
                async (UpdateWarehouseRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Warehouse))
            .WithName("Update Warehouse")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok> HandleAsync(UpdateWarehouseRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateWarehouseCommand(request.Id, request.Name, request.Location, request.Capacity);

        await sender.Send(command, cancellationToken);

        return TypedResults.Ok();
    }
}
