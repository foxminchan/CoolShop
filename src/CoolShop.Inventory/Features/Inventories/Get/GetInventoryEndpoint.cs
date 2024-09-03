namespace CoolShop.Inventory.Features.Inventories.Get;

public sealed class GetInventoryEndpoint : IEndpoint<Ok<InventoryDto>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/inventories/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<InventoryDto>>()
            .WithTags(nameof(Inventory))
            .WithName("Get Inventory")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<InventoryDto>> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetInventoryQuery(id), cancellationToken);

        return TypedResults.Ok(result.Value?.ToDto());
    }
}
