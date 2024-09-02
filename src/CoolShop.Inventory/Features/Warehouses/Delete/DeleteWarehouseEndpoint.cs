using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Delete;

public sealed class DeleteWarehouseEndpoint : IEndpoint<NoContent, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/warehouses/{id:guid}", async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<NoContent>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Warehouse))
            .WithName("Delete Warehouse")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<NoContent> HandleAsync(Guid id, ISender sender, CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteWarehouseCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}
