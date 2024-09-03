﻿namespace CoolShop.Inventory.Features.Inventories.Delete;

public sealed class DeleteInventoryEndpoint : IEndpoint<NoContent, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/inventories/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<NoContent>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Inventory))
            .WithName("Delete Inventory")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<NoContent> HandleAsync(Guid id, ISender sender, CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteInventoryCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}
