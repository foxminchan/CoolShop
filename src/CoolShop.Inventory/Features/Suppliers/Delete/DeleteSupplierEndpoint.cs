using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Delete;

public sealed class DeleteSupplierEndpoint : IEndpoint<NoContent, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/suppliers/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<NoContent>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Supplier))
            .WithName("Delete Supplier")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<NoContent> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteSupplierCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}
