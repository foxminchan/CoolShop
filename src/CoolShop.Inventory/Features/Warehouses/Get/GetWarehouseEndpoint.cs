using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Get;

public sealed class GetWarehouseEndpoint : IEndpoint<Ok<WarehouseDto>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses/{id:guid}", async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<WarehouseDto>>()
            .WithTags(nameof(Warehouse))
            .WithName("Get Warehouse")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<WarehouseDto>> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetWarehouseQuery(id), cancellationToken);

        return TypedResults.Ok(result.Value);
    }
}
